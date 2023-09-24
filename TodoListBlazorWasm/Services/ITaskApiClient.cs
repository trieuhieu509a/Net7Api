﻿using TodoList.Models;

namespace TodoListBlazorWasm.Services
{
    public interface ITaskApiClient
    {
        Task<List<TaskDto>> GetTaskList();

        Task<TaskDto> GetTaskDetail(string id);
    }
}
