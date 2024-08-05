using MassTransit;
using TaskManager.DataProvider.Services;
using TaskManager.Infrastructure.Dtos;

namespace TaskManager.API.Handlers.UserHandler;

public class MakeTopTaskHandler : IConsumer<MakeTopTaskDto>
{
    private ITaskService _service;
    private readonly IPriorityListService _priorityService;

    public MakeTopTaskHandler(ITaskService service, IPriorityListService priorityService)
    {
        _service = service;
        _priorityService = priorityService;
    }
    
    public async Task Consume(ConsumeContext<MakeTopTaskDto> context)
    {
        var existingTask = await _service.GetUpdateTask(context.Message.Id);
        var newTaskPosition = await CountTasks() + 1;
        existingTask.Position = newTaskPosition;
        var updatedTask = await _service.UpdateTask(existingTask);
    }
    
    private async Task<int> CountTasks()
    {
        return await _service.CountAsync();
    }
}