using Microsoft.AspNetCore.Components;
using TodoList.Models;
using TodoList.Models.Enums;
using TodoListBlazorWasm.Components;
using TodoListBlazorWasm.Services;

namespace TodoListBlazorWasm.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }
        [Inject] private IUserApiClient UserApiClient { set; get; }

        private List<TaskDto> Tasks;

        private TaskListSearch TaskListSearch = new TaskListSearch();
        protected Confirmation DeleteConfirmation { set; get; }
        private Guid DeleteId { set; get; }

        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetTaskList(TaskListSearch);
        }

        private async Task SearchTask(TaskListSearch taskListSearch)
        {
            TaskListSearch = taskListSearch;
            Tasks = await TaskApiClient.GetTaskList(TaskListSearch);
        }
        public void OnDeleteTask(Guid deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }

        public async Task OnConfirmDeleteTask(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await TaskApiClient.DeleteTask(DeleteId);
                Tasks = await TaskApiClient.GetTaskList(TaskListSearch);
            }
        }
    }

}