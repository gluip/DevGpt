using System.Reflection;
using DevGpt.Models.Magic;


namespace DevGpt.OpenAI.Test
{
    public class MagicFunctionTest
    {
        [Fact]
        public async Task MagicFunctions_CanGeneratePrimeNumbers()
        {
            var client = new DotnetOpenAIClient();

            var magicFunction = new MagicFunction(client);
            var result = await magicFunction.GetResults<IList<int>>("Return the first 10 prime numbers", "",
                    new List<int> { 4, 3 });
            
            Assert.Equal(10,result.Count);
        }

        [Fact]
        public async Task MagicFunctions_CanGetFacts()
        {
            var client = new DotnetOpenAIClient();

            var magicFunction = new MagicFunction(client);
            var result = await magicFunction.GetResults<IList<string>>("Return the 3 three most significant atomic facts of the following text", "Rommel was a highly decorated officer in World War I and was awarded the Pour le Mérite for his actions on the Italian Front. In 1937, he published his classic book on military tactics, Infantry Attacks, drawing on his experiences in that war.\r\n\r\nIn World War II, he commanded the 7th Panzer Division during the 1940 invasion of France. His leadership of German and Italian forces in the North African campaign established his reputation as one of the ablest tank commanders of the war, and earned him the nickname der Wüstenfuchs, \"the Desert Fox\". Among his British adversaries he had a reputation for chivalry, and his phrase \"war without hate\" has been uncritically used to describe the North African campaign.[2] A number of historians have since rejected the phrase as a myth and uncovered numerous examples of German war crimes and abuses towards both enemy soldiers and native populations in Africa during the conflict.[3] Other historians note that there is no clear evidence Rommel was involved or aware of these crimes,[4] with some pointing out that the war in the desert, as fought by Rommel and his opponents, still came as close to a clean fight as there was in World War II.[5] He later commanded the German forces opposing the Allied cross-channel invasion of Normandy in June 1944.",
                new List<string> { "Red is a colour","Amsterdam is a city" });

            Assert.Equal(3, result.Count);
        }
    }
}