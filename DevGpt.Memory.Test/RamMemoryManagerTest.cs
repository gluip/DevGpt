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
        var topMessage = manager.RetrieveRelevantMemory("heineken");
        var wimLexMesage = manager.RetrieveRelevantMemory("willem alexander");

    }
}