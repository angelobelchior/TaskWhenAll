var randomDelay = () => Random.Shared.Next(1000, 5000);

var cts = new CancellationTokenSource();

var task0 = Task.Run(async () =>
{
    // Simulo uma tarefa onde o usuário cancela a operação
    Console.WriteLine("Executando Task do Cancellation Token");
    // Forçando para ser a primeira task a ser executada
    await Task.Delay(50);
    cts.Cancel();
});

var task1 = Task.Run(async () =>
{
    Console.WriteLine("Executando Task 1");
    await Task.Delay(randomDelay());
    // Se o token de cancelamento foi solicitado, lança uma exceção
    if (cts.Token.IsCancellationRequested)
        throw new ArgumentNullException("ParameterA", "Deu erro na Task 1");
});

var task2 = Task.Run(async () =>
{
    Console.WriteLine("Executando Task 2");
    await Task.Delay(randomDelay());
    // Se o token de cancelamento foi solicitado, lança uma exceção
    if (cts.Token.IsCancellationRequested)
    {
        // Experimentando outro tipo de exceção... só pra testar mesmo...
        throw new OperationCanceledException("Deu erro na Task 2");
    }
});

var task3 = Task.Run(async () =>
{
    Console.WriteLine("Executando Task 3");
    await Task.Delay(randomDelay());
    // Se o token de cancelamento foi solicitado, lança uma exceção
    if (cts.Token.IsCancellationRequested)
        throw new IndexOutOfRangeException("Deu erro na Task 3");
    
    cts.Token.ThrowIfCancellationRequested();
});

await Task.WhenAll(task0, task1, task2, task3).ContinueWith(task =>
{
    //Status da task de execução
    Console.WriteLine($"{task.Status} - {task.Exception?.GetType().Name} - {task.Exception?.Message}");
    
    Console.WriteLine($"Task1: {task1.Status} - {task1.Exception?.GetType().Name} - {task1.Exception?.Message}");
    Console.WriteLine($"Task2: {task2.Status} - {task2.Exception?.GetType().Name} - {task2.Exception?.Message}");
    Console.WriteLine($"Task3: {task3.Status} - {task3.Exception?.GetType().Name} - {task3.Exception?.Message}");
});

Console.ReadLine();



// var randomDelay = () => Random.Shared.Next(1000, 5000);
//
// var cts = new CancellationTokenSource();
//
// var task0 = Task.Run(async () =>
// {
//     // Simulo uma tarefa onde o usuário cancela a operação
//     Console.WriteLine("Executando Task do Cancellation Token");
//     // Forçando para ser a primeira task a ser executada
//     await Task.Delay(50);
//     cts.Cancel();
// });
//
// var task1 = Task.Run(async () =>
// {
//     Console.WriteLine("Executando Task 1");
//     await Task.Delay(randomDelay());
//     // Se o token de cancelamento foi solicitado, lança uma exceção
//     if (cts.Token.IsCancellationRequested)
//         throw new ArgumentNullException("ParameterA", "Deu erro na Task 1");
// });
//
// var task2 = Task.Run(async () =>
// {
//     Console.WriteLine("Executando Task 2");
//     await Task.Delay(randomDelay());
//     // Se o token de cancelamento foi solicitado, lança uma exceção
//     if (cts.Token.IsCancellationRequested)
//     {
//         // Experimentando outro tipo de exceção... só pra testar mesmo...
//         throw new OperationCanceledException("Deu erro na Task 2");
//     }
// });
//
// var task3 = Task.Run(async () =>
// {
//     Console.WriteLine("Executando Task 3");
//     await Task.Delay(randomDelay());
//     // Se o token de cancelamento foi solicitado, lança uma exceção
//     if (cts.Token.IsCancellationRequested)
//         throw new IndexOutOfRangeException("Deu erro na Task 3");
//     
//     cts.Token.ThrowIfCancellationRequested();
// });
//
// var result = Task.WhenAll(task0, task1, task2, task3);
// try
// {
//     await result;
// }
// catch (Exception e)
// {
//     Console.WriteLine($"{e.GetType().Name} - {e.Message}");
//     Console.WriteLine($"{result.Exception?.GetType().Name} - {result.Exception?.Message}");
//     Console.WriteLine($"Task1: {task1.Status} - {task1.Exception?.GetType().Name} - {task1.Exception?.Message}");
//     Console.WriteLine($"Task2: {task2.Status} - {task2.Exception?.GetType().Name} - {task2.Exception?.Message}");
//     Console.WriteLine($"Task3: {task3.Status} - {task3.Exception?.GetType().Name} - {task3.Exception?.Message}");
// }
//
// Console.ReadLine();