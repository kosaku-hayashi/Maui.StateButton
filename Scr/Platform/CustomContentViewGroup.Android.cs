using Android.Content;
using Android.Views;
using Java.Lang;
using Microsoft.Maui.Platform;
using String = Java.Lang.String;

namespace IeuanWalker.Maui.StateButton.Platform;
public class CustomContentViewGroup : ContentViewGroup
{
	Rect _rect;
	readonly StateButton _stateButton;

    bool _longPressHandled = false;

	public CustomContentViewGroup(Context context, IBorderView virtualView) : base(context)
	{
		_stateButton = (StateButton)virtualView;

		Clickable = true;

		//! important this click is only for accessibility
		Click += (sender, e) => _stateButton.InvokeClicked();

		Touch += (sender, te) =>
		{
			if (sender is not Android.Views.View view)
			{
				return;
			}

			switch (te?.Event?.Action)
			{
				case MotionEventActions.Down:
					_rect = new Rect(view.Left, view.Top, view.Right, view.Bottom);
					_longPressHandled = false;
					_stateButton.InvokePressed();
					te.Handled = false;
					break;

				case MotionEventActions.Up:
					if (_rect.Contains(view.Left + (int)te.Event.GetX(), view.Top + (int)te.Event.GetY()))
					{
						_stateButton.InvokeReleased();
						if(!_longPressHandled)
						{
							_stateButton.InvokeClicked();
						}
					}
					else
					{
						_stateButton.InvokeReleased();
					}
					break;

				case MotionEventActions.Cancel:
					_stateButton.InvokeReleased();

					break;

				case MotionEventActions.Move:
					if (!_rect.Contains(view.Left + (int)te.Event.GetX(), view.Top + (int)te.Event.GetY()))
					{
						_stateButton.InvokeReleased();
					}

					break;
			}
		};

		LongClick += (sender, e) =>
		{
			_stateButton.InvokeLongPressed();
			_longPressHandled = true;
			e.Handled = true;
		};
	}

	public override ICharSequence? AccessibilityClassNameFormatted => new String("android.widget.Button");
}