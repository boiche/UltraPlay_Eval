using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using UltraPlay_evaluation.Data;
using UltraPlay_evaluation.Data.Entities;

namespace UltraPlay_evaluation
{
    public class DataFetchUnitOfWork
    {
        private readonly UltraPlay_EvalContext _context;
        private readonly IMapper _mapper;

        public XmlSports Model { get; set; }

        public DataFetchUnitOfWork(UltraPlay_EvalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public DataFetchUnitOfWork Serve<T>() where T : BaseEntity
        {
            var currentEntities = _context.Set<T>().ToList();
            List<T> incomingEntities = Model.GetLayer<T>(_mapper);
            var entitiesToAdd = incomingEntities.Where(x => !currentEntities.Select(x => x.ID).Contains(x.ID));
            var entitiesToRemove = currentEntities.Where(x => !incomingEntities.Select(x => x.ID).Contains(x.ID));

            foreach (var itemToAdd in entitiesToAdd)
            {
                var entryToAdd = _context.Attach(itemToAdd);
                entryToAdd.State = EntityState.Added;
            }
            foreach (var itemToRemove in entitiesToRemove)
            {
                var entryToRemove = _context.Entry(itemToRemove);
                entryToRemove.State = EntityState.Deleted;
            }

            return this;
        }

        public void UpdateDatabase()
        {
            _context.SaveChanges();
        }
    }
}
