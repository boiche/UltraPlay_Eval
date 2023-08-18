using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.Data.Entities;
using UltraPlay_evaluation.QueueService;

namespace UltraPlay_evaluation.Utils
{
    public class DataFetchUnitOfWork
    {
        private readonly UltraPlay_EvalContext _context;
        private readonly IMapper _mapper;

        [Obsolete("Replaced with XDocument structure")]
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

                if (!AreEqual(entryToUpdate.Entity, updateWith))
                {
                    entryToUpdate.CurrentValues.SetValues(updateWith);
                    entryToUpdate.State = EntityState.Modified;
                    QueueService.QueueOutdatedAsync(x => entryToUpdate.Entity);
                }
            }

            return this;
        }

        public void UpdateDatabase()
        {
            _context.SaveChanges();
        }

        private static bool AreEqual<T>(T first, T second)
        {
            bool result = true;
            foreach (var property in first.GetType().GetProperties().Where(x => !x.HasAttribute<InversePropertyAttribute>()))
            {
                var firstValue = property.GetValue(first);
                var secondValue = property.GetValue(second);

                if (firstValue is null && secondValue is not null ||
                    firstValue is not null && secondValue is null)
                    return false;

                if (firstValue is null && secondValue is null)
                    continue;

                if (!firstValue.Equals(secondValue))
                    return false;
            }

            return result;
        }
    }
}
