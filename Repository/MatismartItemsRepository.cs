using ModbusBackend.Models;

namespace ModbusBackend.Repository
{
    // TODO: create abstract class and move this to it's implementation
    public class MatismartItemsRepository
    {
        public readonly List<MatismartRecloserItemModel> items = new();
        public MatismartItemsRepository()
        {
            var item = new MatismartRecloserItemModel(118, "test item 1", null, RecloserType.MCB, RecloserControlOver.Light, 0);
            items.Add(item);
            item = new MatismartRecloserItemModel(118, "test item 2", null, RecloserType.MCB, RecloserControlOver.Light, 0);
            items.Add(item);
            item = new MatismartRecloserItemModel(118, "test item 3", null, RecloserType.MCB, RecloserControlOver.Light, 0);
            items.Add(item);
        }
    }
}
