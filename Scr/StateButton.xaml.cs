using System.Windows.Input;
using IeuanWalker.Maui.StateButton.Enums;

namespace IeuanWalker.Maui.StateButton;

public partial class StateButton : Border
{
	#region Bindable Properties

	/// <summary>
	/// Backing BindableProperty for the <see cref="State"/> property.
	/// </summary>
	public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(ButtonState), typeof(StateButton), ButtonState.NotPressed, BindingMode.OneWayToSource);

	/// <summary>
	/// Property that gets updated depending on the button state. This is a bindable property.
	/// </summary>
	public ButtonState State
	{
		get => (ButtonState)GetValue(StateProperty);
		set => SetValue(StateProperty, value);
	}

	#endregion Bindable Properties

	#region Commands

	/// <summary>
	/// Backing BindableProperty for the <see cref="ClickedCommand"/> property.
	/// </summary>
	public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(StateButton));

	/// <summary>
	/// Command that is triggered when the button is clicked. This is a bindable property.
	/// </summary>
	public ICommand ClickedCommand
	{
		get => (ICommand)GetValue(ClickedCommandProperty);
		set => SetValue(ClickedCommandProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="ClickedCommandParameter"/> property.
	/// </summary>
	public static readonly BindableProperty ClickedCommandParameterProperty = BindableProperty.Create(nameof(ClickedCommandParameter), typeof(object), typeof(StateButton));

	/// <summary>
	/// Property that gets returned when  <see cref="ClickedCommand" /> is executed. This is a bindable property.
	/// </summary>
	public object ClickedCommandParameter
	{
		get => GetValue(ClickedCommandParameterProperty);
		set => SetValue(ClickedCommandParameterProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="PressedCommand"/> property.
	/// </summary>
	public static readonly BindableProperty PressedCommandProperty = BindableProperty.Create(nameof(PressedCommand), typeof(ICommand), typeof(StateButton));

	/// <summary>
	/// Command that is triggered when the button is pressed. This is a bindable property.
	/// </summary>
	public ICommand PressedCommand
	{
		get => (ICommand)GetValue(PressedCommandProperty);
		set => SetValue(PressedCommandProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="PressedCommandParameter"/> property.
	/// </summary>
	public static readonly BindableProperty PressedCommandParameterProperty = BindableProperty.Create(nameof(PressedCommandParameter), typeof(object), typeof(StateButton));

	/// <summary>
	/// Property that gets returned when  <see cref="PressedCommand" /> is executed. This is a bindable property.
	/// </summary>
	public object PressedCommandParameter
	{
		get => GetValue(PressedCommandParameterProperty);
		set => SetValue(PressedCommandParameterProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="ReleasedCommand"/> property.
	/// </summary>
	public static readonly BindableProperty ReleasedCommandProperty = BindableProperty.Create(nameof(ReleasedCommand), typeof(ICommand), typeof(StateButton));

	/// <summary>
	/// Command that is triggered when the button is released. This is a bindable property.
	/// </summary>
	public ICommand ReleasedCommand
	{
		get => (ICommand)GetValue(ReleasedCommandProperty);
		set => SetValue(ReleasedCommandProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="ReleasedCommandParameter"/> property.
	/// </summary>
	public static readonly BindableProperty ReleasedCommandParameterProperty = BindableProperty.Create(nameof(ReleasedCommandParameter), typeof(object), typeof(StateButton));

	/// <summary>
	/// Property that gets returned when  <see cref="ReleasedCommand" /> is executed. This is a bindable property.
	/// </summary>
	public object ReleasedCommandParameter
	{
		get => GetValue(ReleasedCommandParameterProperty);
		set => SetValue(ReleasedCommandParameterProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="LongPressCommand"/> property.
	/// </summary>
	public static readonly BindableProperty LongPressCommandProperty = BindableProperty.Create(nameof(LongPressCommand), typeof(ICommand), typeof(StateButton));

	/// <summary>
	/// Command that is triggered when the button is long pressed. This is a bindable property.
	/// </summary>
	public ICommand LongPressCommand
	{
		get => (ICommand)GetValue(LongPressCommandProperty);
		set => SetValue(LongPressCommandProperty, value);
	}

	/// <summary>
	/// Backing BindableProperty for the <see cref="LongPressCommandParameter"/> property.
	/// </summary>
	public static readonly BindableProperty LongPressCommandParameterProperty = BindableProperty.Create(nameof(LongPressCommandParameter), typeof(object), typeof(StateButton));

	/// <summary>
	/// Property that gets returned when  <see cref="LongPressCommand" /> is executed. This is a bindable property.
	/// </summary>
	public object LongPressCommandParameter
	{
		get => GetValue(LongPressCommandParameterProperty);
		set => SetValue(LongPressCommandParameterProperty, value);
	}

	#endregion Commands

	#region Events

	/// <summary>
	/// Event that is triggered when button is pressed. This is a bindable property.
	/// </summary>
	public event EventHandler<EventArgs>? Pressed;

	/// <summary>
	/// Event that is triggered when button is released. This is a bindable property.
	/// </summary>
	public event EventHandler<EventArgs>? Released;

	/// <summary>
	/// Event that is triggered when button is clicked. This is a bindable property.
	/// </summary>
	public event EventHandler<EventArgs>? Clicked;

	/// <summary>
	/// Event that is triggered when the button is long pressed. This is a bindable property.
	/// </summary>
	public event EventHandler<EventArgs>? LongPressed;

	#endregion Events

	public StateButton()
	{
		InitializeComponent();
	}

	internal void InvokePressed()
	{
		if (!IsEnabled)
		{
			return;
		}

		Pressed?.Invoke(this, EventArgs.Empty);
		PressedCommand?.Execute(PressedCommandParameter);

		State = ButtonState.Pressed;
	}

	internal void InvokeReleased()
	{
		if (!IsEnabled)
		{
			return;
		}

		if (State.Equals(ButtonState.NotPressed))
		{
			return;
		}

		Released?.Invoke(this, EventArgs.Empty);
		ReleasedCommand?.Execute(ReleasedCommandParameter);

		State = ButtonState.NotPressed;
	}

	internal void InvokeClicked()
	{
		if (!IsEnabled)
		{
			return;
		}

		Clicked?.Invoke(this, EventArgs.Empty);
		ClickedCommand?.Execute(ClickedCommandParameter);
	}

	internal void InvokeLongPressed()
	{
		if(!IsEnabled)
		{
			return;
		}

		LongPressed?.Invoke(this, EventArgs.Empty);
		LongPressCommand?.Execute(LongPressCommandParameter);
	}
}