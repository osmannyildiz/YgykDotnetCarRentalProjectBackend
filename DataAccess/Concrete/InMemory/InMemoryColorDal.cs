using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory {
    public class InMemoryColorDal : IColorDal {
        List<Color> _colors;

        public InMemoryColorDal() {
            _colors = new List<Color> {
                new Color {Id=1, Name="Beyaz"},
                new Color {Id=2, Name="Gri"}
            };
        }

        public void Add(Color color) {
            _colors.Add(color);
        }

        public void Delete(Color color) {
            Color colorToDelete = _colors.SingleOrDefault(c => c.Id == color.Id);
            _colors.Remove(colorToDelete);
        }

        public List<Color> GetAll() {
            return _colors;
        }

        public Color GetById(int id) {
            return _colors.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Color color) {
            Color colorToUpdate = _colors.SingleOrDefault(b => b.Id == color.Id);
            colorToUpdate.Id = color.Id;
            colorToUpdate.Name = color.Name;
        }
    }
}
