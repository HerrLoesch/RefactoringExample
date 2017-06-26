using TechTalk.SpecFlow;

namespace RefactoringExample.UiTests
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            StartApplication();
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
