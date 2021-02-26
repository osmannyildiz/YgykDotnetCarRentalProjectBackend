using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory {
    public class InMemoryColorDal : IColorDal {
        List<Color> _colors;

        public InMemoryColorDal() {
            _colors = new List<Color> {
                new Color {Id=1, Name="Beyaz"},
                new Color {Id=2, Name="Gri"},
                new Color {Id=3, Name="Kırmızı"}
            };
        }

        public void Add(Color color) {
            _colors.Add(color);
        }

        public void Delete(Color color) {
            Color colorToDelete = _colors.SingleOrDefault(c => c.Id == color.Id);
            _colors.Remove(colorToDelete);
        }

        public Color Get(Expression<Func<Color, bool>> filter) {
            return _colors.SingleOrDefault(filter.Compile());
        }

        public List<Color> GetAll(Expression<Func<Color, bool>> filter = null) {
            if (filter == null) {
                return _colors;
            } else {
                return _colors.Where(filter.Compile()).ToList();
            }
        }

        public void Update(Color color) {
            Color colorToUpdate = _colors.SingleOrDefault(b => b.Id == color.Id);
            colorToUpdate.Id = color.Id;
            colorToUpdate.Name = color.Name;
        }
    }
}
