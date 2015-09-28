using TechTalk.SpecFlow;

namespace RefactoringExample.UiTests
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            CreateDataBase();

            StartApplication();
        }

        private static void CreateDataBase()
        {
            using (var context = new PersonContext())
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                }

                context.Database.Create();
            }
        }

        private static void StartApplication()
        {
            var uiMap = new UIMap();
            uiMap.StartApplication();
            ScenarioContext.Current.Set(uiMap);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ScenarioContext.Current.Get<UIMap>().CloseApplication();
        }
    }
}
