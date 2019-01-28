using System;
using System.Windows;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Galagram.ViewModel
{
#pragma warning disable 1570
    /// <summary>
    /// Helps to pass event arguments to a command
    /// </summary>
    /// <example>
    /// This sample shows how to call the <see cref="EventToCommandBehavior"/> class.
    /// <code>
    /// xmlns:i="http://schemas.microsoft.com/expression/2010/interactivity"
    /// 
    /// <i:Interaction.Behaviors>
    ///     <command:EventToCommandBehavior Command = "{Binding DropCommand}" Event="Drop" PassArguments="True" />
    /// </i:Interaction.Behaviors>
    /// </code>
    /// </example>
 #pragma warning restore 1570
    public class EventToCommandBehavior : Behavior<FrameworkElement>
    {
        // FIELDS
        private Delegate handler;
        private EventInfo oldEvent;

        // DEPENDENCY PROPERTIES
        // Event
        #region Event
        /// <summary>
        /// Gets or sets event name on which command should be executed
        /// </summary>
        public string Event
        {
            get
            {
                return (string)GetValue(EventProperty);
            }
            set
            {
                SetValue(EventProperty, value);
            }
        }
        /// <summary>
        /// Dependency property of <see cref="Event"/>
        /// </summary>
        public static readonly DependencyProperty EventProperty = 
            DependencyProperty.Register(
                name: nameof(Event), 
                propertyType: typeof(string), 
                ownerType: typeof(EventToCommandBehavior), 
                typeMetadata: new PropertyMetadata(defaultValue: null, 
                                                   propertyChangedCallback: OnEventChanged));
        #endregion

        // Command
        #region Command
        /// <summary>
        /// Gets or sets command to execute on event
        /// </summary>
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }
        /// <summary>
        /// Dependency property for <see cref="Command"/>
        /// </summary>
        public static readonly DependencyProperty CommandProperty = 
            DependencyProperty.Register(
                name: nameof(Command), 
                propertyType: typeof(ICommand), 
                ownerType: typeof(EventToCommandBehavior), 
                typeMetadata: new PropertyMetadata(defaultValue: null));
        #endregion

        // PassArguments (default: false)
        #region PassArguments
        /// <summary>
        /// Gets or sets value should event arguments be passed to a command as parameter
        /// </summary>
        public bool PassArguments
        {
            get
            {
                return (bool)GetValue(PassArgumentsProperty);
            }
            set
            {
                SetValue(PassArgumentsProperty, value);
            }
        }
        /// <summary>
        /// Dependecy property for <see cref="PassArguments"/>
        /// </summary>
        public static readonly DependencyProperty PassArgumentsProperty = 
            DependencyProperty.Register(
                name: nameof(PassArguments), 
                propertyType: typeof(bool), 
                ownerType: typeof(EventToCommandBehavior), 
                typeMetadata: new PropertyMetadata(defaultValue: false));
        #endregion

        // METHODS

        private static void OnEventChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            EventToCommandBehavior behavior = (EventToCommandBehavior)dependencyObject;

            // is not yet attached at initial load
            if (behavior.AssociatedObject != null)
            {
                behavior.AttachHandler((string)dependencyPropertyChangedEventArgs.NewValue);
            }
        }
        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            AttachHandler(this.Event); // initial set
        }

        /// <summary>
        /// Attaches the handler to the event
        /// </summary>
        private void AttachHandler(string eventName)
        {
            // detach old event
            if (oldEvent != null)
            {
                oldEvent.RemoveEventHandler(this.AssociatedObject, handler);
            }

            // attach new event
            if (!string.IsNullOrEmpty(eventName))
            {
                EventInfo eventInfo = this.AssociatedObject.GetType().GetEvent(eventName);
                if (eventInfo != null)
                {
                    MethodInfo methodInfo = this.GetType().GetMethod("ExecuteCommand", BindingFlags.Instance | BindingFlags.NonPublic);
                    handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, methodInfo);
                    eventInfo.AddEventHandler(this.AssociatedObject, handler);
                    oldEvent = eventInfo; // store to detach in case the Event property changes
                }
                else throw new ArgumentException(string.Format(Core.Messages.Error.ViewModel.EVENT_OF_TYPE_IS_NOT_FOUND_FORMAT, eventName, this.AssociatedObject.GetType().Name));
            }
        }

        /// <summary>
        /// Executes the Command
        /// </summary>
        private void ExecuteCommand(object sender, EventArgs e)
        {
            // prepare parameters for command
            object parameter = this.PassArguments ? e : null;
            
            // execute command if can
            if (this.Command != null && this.Command.CanExecute(parameter))
            {
                this.Command.Execute(parameter);
            }
        }
    }
}
