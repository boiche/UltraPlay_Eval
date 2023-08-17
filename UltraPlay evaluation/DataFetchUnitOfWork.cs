using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using System.Xml.Linq;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.Data.Entities;
using UltraPlay_evaluation.Utils;

namespace UltraPlay_evaluation
{
    public class DataFetchUnitOfWork
    {
        private readonly UltraPlay_EvalContext _context;
        private readonly IMapper _mapper;

        [Obsolete("Replaced with XDocument structure")]
        public XmlSports? Model { get; set; }
        public XDocument? XDocument { get; internal set; }
        public IQueueService QueueService { get; internal set; }

        public DataFetchUnitOfWork(UltraPlay_EvalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataFetchUnitOfWork Serve<T>() where T : BaseEntity
        {
            var currentEntities = _context.Set<T>().ToList();
            var incomingXDocument = XDocument.GetLayer<T>();

            var entitiesToAdd = incomingXDocument.Where(x => !currentEntities.Select(x => x.ID).Contains(x.ID));
            var entitiesToRemove = currentEntities.Where(x => !incomingXDocument.Select(x => x.ID).Contains(x.ID));
            var entitiesToUpdate = currentEntities.Where(x => incomingXDocument.Select(x => x.ID).Contains(x.ID));

            foreach (var itemToAdd in entitiesToAdd)
            {
                var entryToAdd = _context.Attach(itemToAdd);
                entryToAdd.Entity.IsActive = true;
                entryToAdd.State = EntityState.Added;
            }
            foreach (var itemToRemove in entitiesToRemove)
            {
                var entryToRemove = _context.Entry(itemToRemove);
                entryToRemove.Entity.IsActive = false;
                entryToRemove.State = EntityState.Detached;
                QueueService.QueueHiddenAsync(x => entryToRemove.Entity);
            }
            foreach (var itemToUpdate in entitiesToUpdate)
            {
                var entryToUpdate = _context.Entry(itemToUpdate);
                var updateWith = incomingXDocument.First(x => x.ID == itemToUpdate.ID);
                entryToUpdate.CurrentValues.SetValues(updateWith);
                entryToUpdate.State = EntityState.Modified;
                QueueService.QueueOutdatedAsync(x => entryToUpdate.Entity);
            }

            return this;
        }

        public void UpdateDatabase()
        {
            _context.SaveChanges();
        }
    }
}
