using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using UI.AutomationChallange.PageObjects;

namespace UI.AutomationChallange.Tests
{
    public class EventHandlers
    {
        public By LastByLocatorUsed { get; set; } = null!;
        private By LastByLocatorCompleted { get; set; } = null!;
        private string LastElementUsed => LastByLocatorUsed.ToString().Split(':')[1].Trim();
        public void EventDriver_ElementValueChanged(object sender, WebElementValueEventArgs e)
        {
            new LoginPage().WriteTimeStampedLine($"ElementValueChanged: {LastElementUsed}; Value: {e.Value}");
        }
        public void EventDriver_ElementClicked(object sender, WebElementEventArgs e)
        {
            new LoginPage().WriteTimeStampedLine($"ElementClicked: {LastElementUsed}");
        }

        public static void EventDriver_FindingElement(object sender, FindElementEventArgs e)
        {
            //WriteTimeStampedLine($"FindingElement: {e.FindMethod}");
            //LastByLocatorUsed = e.FindMethod;
        }

        public void EventDriver_FindElementCompleted(object sender, FindElementEventArgs e)
        {
            if (e.FindMethod != LastByLocatorCompleted &&
                e.FindMethod.ToString().Contains("error") == false &&
                e.FindMethod.ToString().Contains("crashed") == false &&
                e.FindMethod.ToString().Contains("Loader") == false)
            {
                new LoginPage().WriteTimeStampedLine($"FindElementCompleted: {e.FindMethod}");
            }

            LastByLocatorCompleted = e.FindMethod;
        }
    }
}
