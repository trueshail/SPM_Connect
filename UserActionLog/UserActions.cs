using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace SPMConnect.UserActionLog
{
    public class UserActions
    {
        #region "Private Member Variables"

        //The StackFrameCounttoGetMethodName depends, usually its the 1st Frame but in some framework (eg CSLA) its the 2nd Frame
        private readonly int _stackFrameCountToGetMethodName = 1;

        private readonly Type _frmType;
        private Form _frm;

        //Choose your preferred Logging , Log4Net, Elmah, etc TODO do this a Config switch
        private ILog _logger = new Logger();

        #endregion "Private Member Variables"

        #region "Constructor"

        /// <summary>
        /// Ctor Easy/Lazy way of hooking up all form control events to listen for user actions.
        /// </summary>
        /// /// <param name="frm">The WinForm, WPF, Xamarin, etc Form.</param>
        public UserActions(Control frm, int StackFrameCountToGetMethodName = 1)
        {
            _frmType = ((Form)frm).GetType();
            _frm = (Form)frm;
            ActionLoggerSetUp(frm);
            this._stackFrameCountToGetMethodName = StackFrameCountToGetMethodName;
        }

        /// <summary>
        /// Ctor Optimal way of hooking up control events to listen for user actions.
        /// </summary>
        public UserActions(Control[] ctrls, int stackFrameCountToGetMethodName = 1)
        {
            ActionLoggerSetUp(ctrls);
            if (stackFrameCountToGetMethodName != 1) this._stackFrameCountToGetMethodName = stackFrameCountToGetMethodName;
        }

        /// <summary>
        /// Lazy way of hooking up all form control events to listen for user actions.
        /// </summary>
        /// /// <param name="parentCtrl">The WinForm, WPF, Xamarin, etc Form.</param>
        private void ActionLoggerSetUp(Control parentCtrl)
        {
            HookUpEvents(parentCtrl);
            foreach (Control ctrl in parentCtrl.Controls)
            {
                ActionLoggerSetUp(ctrl); //Recursively hook up control events
            }
        }

        /// <summary>
        /// Optional/NonRecursive way of hooking up specific control events to listen for user actions.
        /// </summary>
        /// <param name="ctrls">The controls on the ASP.Net, WinForm, WPF, Xamarin, etc Form.<param>
        private void ActionLoggerSetUp(params Control[] ctrls)
        {
            foreach (var ctrl in ctrls) HookUpEvents(ctrl);
        }

        #endregion "Constructor"

        #region "Methods"

        /// <summary>
        /// Hooks up the event(s) to get the steps to reproduce problems.
        /// </summary>
        /// <param name="ctrl">The control whose events we're suspicious of causing problems.</param>
        private void HookUpEvents(Control ctrl)
        {
            if (ctrl is TextBoxBase txt)
            { //TextBoxBase stands for Textboxes, MaskedBoxes and various other text input controls too.
                txt.Enter += LogAction;
            }
            else if (ctrl is ListControl lst)
            { //ListControl stands for ComboBoxes and ListBoxes.
                lst.SelectedValueChanged += LogAction;
            }
            else if (ctrl is ButtonBase btn)
            { //ButtonBase stands for Buttons, CheckBoxes and RadioButtons.
                btn.Click += LogAction;
            }
            else if (ctrl is DateTimePicker dtp)
            {
                dtp.Enter += LogAction;
                dtp.ValueChanged += LogAction;
            }
            else if (ctrl is DataGridView dgv)
            {
                dgv.RowEnter += LogAction;
                dgv.CellBeginEdit += LogAction;
                dgv.CellEndEdit += LogAction;
            }
            else if (ctrl is TreeView tv)
            {
                tv.BeforeSelect += LogAction;
                tv.BeforeCollapse += LogAction;
                tv.BeforeExpand += LogAction;
            }
            else if (ctrl is Form frm)
            {
                frm.Load += LogAction;
                frm.FormClosing += LogAction;
                frm.Resize += LogAction;
                frm.ResizeBegin += LogAction;
                frm.ResizeEnd += LogAction;
            }
        }

        /// <summary>
        /// Releases the hooked up events (avoiding memory leaks).
        /// </summary>
        /// <param name="ctrl"></param>
        private void ReleaseEvents(Control ctrl)
        {
            if (ctrl is TextBoxBase txt)
            {
                txt.Enter -= LogAction;
            }
            else if (ctrl is ButtonBase btn)
            {
                btn.Click -= LogAction;
            }
            else if (ctrl is ListControl lst)
            {
                lst.SelectedValueChanged -= LogAction;
            }
            else if (ctrl is DateTimePicker dtp)
            {
                dtp.Enter -= LogAction;
                dtp.ValueChanged -= LogAction;
            }
            else if (ctrl is DataGridView dgv)
            {
                dgv.RowEnter -= LogAction;
                dgv.CellBeginEdit -= LogAction;
                dgv.CellEndEdit -= LogAction;
            }
            else if (ctrl is TreeView tv)
            {
                tv.BeforeSelect -= LogAction;
                tv.BeforeCollapse -= LogAction;
                tv.BeforeExpand -= LogAction;
            }
            else if (ctrl is Form frm)
            {
                frm.Load -= LogAction;
                frm.FormClosing -= LogAction;
                frm.Resize -= LogAction;
                frm.ResizeBegin -= LogAction;
                frm.ResizeEnd -= LogAction;
            }
        }

        /// <summary>
        /// Log the Control that made the call and its value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LogAction(object sender, EventArgs e)
        {
            #region "Custom control of what is logged that depends on project/solution"

            //if (!(sender is Form || sender is Button || sender is DataGridView))
            //{   //dont log control events if its a Maintenance Form and its not in Edit mode
            //    if (_frmType.BaseType.ToString().Contains("frmMaint"))
            //    {
            //        PropertyInfo pi = _frmType.GetProperty("IsEditing");
            //        bool isEditing = (bool)pi.GetValue(_frm, null);
            //        if (!isEditing) return;
            //    }
            //}

            #endregion "Custom control of what is logged that depends on project/solution"

            StackTrace stackTrace = new StackTrace();

            StackFrame[] stackFrames = stackTrace.GetFrames();
            var eventName = stackFrames[_stackFrameCountToGetMethodName].GetMethod().Name;
            _logger.LogAction(DateTime.Now, _frm.Name, ((Control)sender).Name, eventName, GetSendingCtrlValue(((Control)sender), eventName));
        }

        private string GetSendingCtrlValue(Control ctrl, string eventType)
        {
            if (ctrl is TextBoxBase)
            {
                return ((TextBoxBase)ctrl).Text;
            }
            else if (ctrl is CheckBox || ctrl is RadioButton)
            {
                return ((ButtonBase)ctrl).Text;
            }
            else if (ctrl is ListControl)
            {
                return ((ListControl)ctrl).Text.ToString();
            }
            else if (ctrl is DateTimePicker)
            {
                return ((DateTimePicker)ctrl).Text;
            }
            else if (ctrl is TreeView)
            {
                return ((TreeView)ctrl).SelectedNode?.Text;
            }
            else if (ctrl is DataGridView && eventType == "OnRowEnter")
            {
                if (((DataGridView)ctrl).SelectedRows.Count > 0)
                {
                    return ((DataGridView)ctrl).SelectedRows[0].Cells[0].Value?.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else if (ctrl is DataGridView)
            {
                DataGridViewCell cell = (((DataGridView)ctrl).CurrentCell);
                return cell?.Value?.ToString();
            }
            else if (ctrl is Form)
            {
                if (eventType == "OnResize") return ((Form)ctrl).WindowState.ToString();
            }
            return string.Empty;
        }

        public void FinishLoggingUserActions(Control frm)
        {
            _logger.WriteLogActionsToSQL();
            ReleaseEvents(frm);
        }
    }

    #endregion "Methods"
}