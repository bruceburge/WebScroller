using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using SharedProject;

namespace ScrollerServiceConsole
{
    class ScheduleTaskManagerOutLook : IManagableScheduleTask
    {
        private List<ScheduleTask> _tasks = new List<ScheduleTask>();
        public List<ScheduleTask> Tasks
        {
            get
            {
                return _tasks;
            }

            set
            {
                _tasks = value;
            }
        }

        public List<ScheduleTask> GetAllScheduleTask()
        {
            Reload();
            return Tasks;
        }

        public ScheduleTask GetScheduleTaskByID(int id)
        {
            return Tasks.FirstOrDefault(x => x.id == id);
        }

        public void Load()
        {
            int lengthOfDisplayString = 40;
            Outlook.NameSpace ns = null;
            Outlook.MAPIFolder todoFolder = null;
            Outlook.Items todoFolderItems = null;
            Outlook.TaskItem task = null;
            Outlook.ContactItem contact = null;
            Outlook.MailItem email = null;
            string todoString = string.Empty;

            try
            {
                Tasks.Clear();
                var OutlookApp = new Microsoft.Office.Interop.Outlook.Application();
                ns = OutlookApp.Session;
                todoFolder = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderToDo);
                todoFolderItems = todoFolder.Items;

                for (int i = 1; i <= todoFolderItems.Count; i++)
                {
                    object outlookItem = todoFolderItems[i];
                    if (outlookItem is Outlook.MailItem)
                    {
                        email = outlookItem as Outlook.MailItem;
                        if (email.FlagStatus != OlFlagStatus.olFlagComplete)
                        {
                            Tasks.Add(new ScheduleTask {id=i, description="Email: "+ email.Subject, dueDate= email.TaskDueDate, title = StringUtilities.TruncateWithEllipsis(email.Subject, lengthOfDisplayString) });
                        }
                    }
                    else if (outlookItem is Outlook.ContactItem)
                    {
                        contact = outlookItem as Outlook.ContactItem;
                        Tasks.Add(new ScheduleTask { id = i, description = "Contact: " + contact.FullName, dueDate = contact.TaskDueDate, title = StringUtilities.TruncateWithEllipsis(contact.FullName, lengthOfDisplayString) });
                    }
                    else if (outlookItem is Outlook.TaskItem)
                    {
                        task = outlookItem as Outlook.TaskItem;
                        if (task.Status != OlTaskStatus.olTaskDeferred && task.Status != OlTaskStatus.olTaskComplete && task.Status != OlTaskStatus.olTaskWaiting)
                        {
                            Tasks.Add(new ScheduleTask { id = i, description = "Task: " + task.Subject, dueDate = task.DueDate, status=task.Status.ToString(), title = StringUtilities.TruncateWithEllipsis(task.Subject, lengthOfDisplayString) });
                        }
                    }
                    else
                    //System.Windows.MessageBox.Show("Unknown Item type");
                    Marshal.ReleaseComObject(outlookItem);
                }
            }
            finally
            {
                if (todoFolderItems != null)
                    Marshal.ReleaseComObject(todoFolderItems);
                if (todoFolder != null)
                    Marshal.ReleaseComObject(todoFolder);
                if (ns != null)
                    Marshal.ReleaseComObject(ns);
            }           
        }
    

        public void Reload()
        {
            Load();
        }

        public int RemoveScheduleTaskByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
