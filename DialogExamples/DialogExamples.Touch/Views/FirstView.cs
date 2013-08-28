using System.Drawing;
using System.Linq;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Touch.Views;
using CrossUI.Touch.Dialog.Elements;
using DialogExamples.Core.ViewModels;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace DialogExamples.Touch.Views
{
	[Register("FirstView")]
	public class FirstView : MvxDialogViewController
	{
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var bindings = this.CreateInlineBindingTarget<FirstViewModel>();

			// note that this list isn't bound - if the view model list changes, then the UI won't update it;s list
			var radioChoices = from r in (ViewModel as FirstViewModel).DessertChoices
							   select (Element)new RadioElement(r);

			var radioGroup = new RootElement("Dessert", new RadioGroup("Dessert", 0)) {
				new Section()
				{
					radioChoices
				}
			}.Bind(bindings, e => e.RadioSelected, vm => vm.CurrentDessertIndex);

			Root = new RootElement("Example Root")
                {
                    new Section("Your details")
                        {
                            new EntryElement("Login", "Enter Login name").Bind(bindings, vm => vm.TextProperty),
                            new EntryElement("Password", "Enter Password", "", true).Bind(bindings, vm => vm.PasswordProperty)
                        },
                    new Section("Your options")
                        {
                            new BooleanElement("Remember me?", false).Bind(bindings, vm => vm.SwitchThis),
                            new CheckboxElement("Upgrade?").Bind(bindings, vm => vm.CheckThis),
                        },
					new Section("Radio")
					{
						(Element)radioGroup
					},
                    new Section("Action")
                        {
                            new StringElement("Second").Bind(bindings, element => element.SelectedCommand, vm => vm.GoSecondCommand),
							//styled button, for <= 3.0.10 you need to use this constructor to 
							// > 3.0.10 can use new StyledStringElement("Bindable Elements")
							new StyledStringElement("Bindable Elements", null, UITableViewCellStyle.Default) {
								BackgroundColor = UIColor.Blue,
								TextColor = UIColor.White,
								Alignment = UITextAlignment.Center
								}.Bind(bindings, element => element.SelectedCommand, vm => vm.BindableElementsCommand),
		                    },
                    new Section("Debug out:")
                        {
                            new StringElement("Login is:").Bind(bindings, vm => vm.TextProperty),
                            new StringElement("Password is:").Bind(bindings, vm => vm.PasswordProperty),
                            new StringElement("Remember is:").Bind(bindings, vm => vm.SwitchThis),
                            new StringElement("Upgrade is:").Bind(bindings, vm => vm.CheckThis),
                            new StringElement("Selected Dessert Index is:").Bind(bindings, vm => vm.CurrentDessertIndex),
                        },
                };
		}
	}
}