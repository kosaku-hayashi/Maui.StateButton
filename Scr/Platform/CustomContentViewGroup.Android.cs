using System.Buffers;
using Android.Content;
using Android.Views;
using Java.Lang;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using String = Java.Lang.String;

namespace IeuanWalker.Maui.StateButton.Platform;

public class CustomContentViewGroup : ContentViewGroup
{
	readonly StateButton _stateButton;
	Rect _rect;

	bool _longPressHandled = false;
	bool _clickHandled = false;

	public CustomContentViewGroup(Context context, IBorderView virtualView) : base(context)
	{
		_stateButton = (StateButton)virtualView;
		Clickable = true;

		//! important this click is only for accessibility
		Click += OnClick;
		Touch += OnTouch;
		LongClick += OnLongClick;
	}

	void OnClick(object? sender, EventArgs e)
	{
		_stateButton.InvokeClicked();
	}

	void OnTouch(object? sender, TouchEventArgs e)
	{
		if(sender is not Android.Views.View view)
		{
			return;
		}

		if(_rect == default)
		{
			int[] location = ArrayPool<int>.Shared.Rent(2);
			view.GetLocationOnScreen(location);
			ArrayPool<int>.Shared.Return(location);
			_rect = new Rect(location[0], location[1], view.Width, view.Height);
		}

		switch(e?.Event?.Action)
		{
			case MotionEventActions.Down:
				_longPressHandled = false;
				_clickHandled = false;
				_stateButton.InvokePressed();
				e.Handled = false;
				break;

			case MotionEventActions.Up:
				_stateButton.InvokeReleased();
				if(!_longPressHandled)
				{
					_stateButton.InvokeClicked();
				}

				_clickHandled = true;
				break;

			case MotionEventActions.Cancel:
				if(!_rect.Contains((int)e.Event.GetX(), (int)e.Event.GetY()))
				{
					_clickHandled = true;
				}
				_stateButton.InvokeReleased();
				break;
		}
	}

	void OnLongClick(object? sender, LongClickEventArgs e)
	{
		if(!_clickHandled)
		{
			_stateButton.InvokeLongPressed();
			_longPressHandled = true;
			e.Handled = true;
		}
	}

	protected override void Dispose(bool disposing)
	{
		Click -= OnClick;
		Touch -= OnTouch;
		LongClick -= OnLongClick;
		base.Dispose(disposing);
	}

	public override ICharSequence? AccessibilityClassNameFormatted => new String("android.widget.Button");
}