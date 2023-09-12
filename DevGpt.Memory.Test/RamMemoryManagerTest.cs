namespace DevGpt.Memory.Test;

public class RamMemoryManagerTest
{
    [Fact]
    public void StoreMessage()
    {
        var manager = new RamMemoryManager();
        manager.StoreMessage("Brie is lekker");
        manager.StoreMessage("Bier is lekker");
        manager.StoreMessage("Kaas is lekker");
        manager.StoreMessage("Koningin beatrix");

        // retrieve
        var topMessage = manager.RetrieveRelevantMessages("heineken");
        var wimLexMesage = manager.RetrieveRelevantMessages("willem alexander");

    }
}