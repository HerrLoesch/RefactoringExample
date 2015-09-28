using TechTalk.SpecFlow;

namespace RefactoringExample.UiTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [Binding]
    public class SpeichernDerPersonenstammdatenSteps
    {
        [Given(@"ich gebe die Daten für (.*) (.*), geboren am (.*) ein")]
        public void AngenommenIchGebeDieDatenFurHansPeterGeborenAm_Ein(string firstName, string lastName, string birthdate)
        {
            var uiMap = ScenarioContext.Current.Get<UIMap>();
            uiMap.EnterFirstNameParams.UIFirstNameTextBoxEditText = firstName;
            uiMap.EnterFirstName();

            uiMap.EnterLastNameParams.UILastNameTextBoxEditText = lastName;
            uiMap.EnterLastName();

            uiMap.EnterBirthdateParams.UIBirthDateTextBoxEditText = birthdate;
            uiMap.EnterBirthdate();
        }

        [When(@"ich auf Speichern klicke")]
        public void WennIchAufSpeichernKlicke()
        {
            ScenarioContext.Current.Get<UIMap>().Save();
        }
        
        [Then(@"soll ein (.*) in der Personen Auswahlliste zu sehen sein")]
        public void DannSollEinHansPeterInDerPersonenAuswahllisteZuSehenSein(string name)
        {
            var uiMap = ScenarioContext.Current.Get<UIMap>();
            Assert.IsTrue(uiMap.ListContains(name));
        }
    }
}
