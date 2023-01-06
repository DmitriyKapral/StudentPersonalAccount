using Microsoft.AspNetCore.Mvc;
using StudentPersonalAccount.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Linq;

namespace StudentPersonalAccount.Controllers;

//[ApiController]
public abstract class BaseCRUDController<T> : ControllerBase
    where T : IGuidKey
{
    /// <summary>
    /// Репозиторий
    /// </summary>
    protected readonly IRepository<T> _repository;

    /// <inheritdoc />
    protected BaseCRUDController(IRepository<T> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Получение списка записей
    /// </summary>
    protected virtual IQueryable<T> List => _repository.GetListQuery();

    /// <summary>
    /// Получение списка всех записей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public virtual IActionResult Get()
    {
        var result = List
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получение списка части записей
    /// </summary>
    /// <param name="limit">Количество записей в ответе (выбираются первые {limit} записей из выборки после {numberSkip} элементов)
    /// <br/><br/>
    /// <b>Max value = 1000</b>
    /// </param>
    /// <param name="numberSkip">Пропускается первых {numberSkip} записей</param>
    /// <returns></returns>
    [HttpGet("piece")]
    public virtual IActionResult ListEntitiesPiece(int limit = 1000, int numberSkip = 0)
    {
        if (limit is < 0 or > 1000) limit = 1000;
        if (numberSkip < 0) numberSkip = 0;

        var result = List
            .Skip(numberSkip)
            .Take(limit)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Получение записи
    /// </summary>
    /// <param name="guid">Идентификатор записи</param>
    /// <returns>Запись</returns>
    [HttpGet("{guid:guid}")]
    public virtual IActionResult Get(Guid guid)
    {
        var entity = List.FirstOrDefault(p => p.Guid == guid);

        if (entity == null) return NotFound("Guid не найден");
        return Ok(entity);
    }

    /// <summary>
    /// Добавление записи
    /// </summary>
    /// <param name="model">Запись</param> 
    /// <returns>Добавленная запись</returns>
    [HttpPost]
    public virtual IActionResult Add(T model)
    {
        if (_repository.Add(model).Guid != Guid.Empty)
            return Ok(model);
        return StatusCode(500, "Произошла ошибка при добавлении записи");
    }

    /// <summary>
    /// Добавление записей
    /// </summary>
    /// <param name="models">Записи</param>
    /// <returns>Добавленная запись</returns>
    [HttpPost("range")]
    public virtual IActionResult AddRange(List<T> models)
    {
        if (_repository.AddRange(models).All(p => p.Guid != Guid.Empty))
            return Ok(models);

        return StatusCode(500, "Произошла ошибка при добавлении записи");
    }

    /// <summary>
    /// Удаление записи
    /// </summary>
    /// <param name="guid">Идентификатор записи</param>
    /// <returns></returns>
    [HttpDelete("{guid}")]
    [HttpDelete("remove/{guid}")]
    public virtual IActionResult RemoveByGuid(Guid guid)
    {
        var entity = _repository.Get(guid);
        if (entity == null) return NotFound();

        if (_repository.Remove(guid))
            return Ok();

        return StatusCode(500, "Произошла ошибка при удалении записи");
    }

    /// <summary>
    /// Удаление записей
    /// </summary>
    /// <param name="guids">Идентификаторы записей</param>
    /// <returns></returns>
    [HttpDelete("range")]
    public virtual IActionResult RemoveRangeByGuid(List<Guid> guids)
    {
        if (guids.Count == 0) return BadRequest();

        if (_repository.RemoveRange(guids))
            return Ok();

        return StatusCode(500, "Произошла ошибка при удалении записи");
    }

    /// <summary>
    /// Обновление записи
    /// </summary>
    /// <param name="model">Обновленная запись</param>
    /// <returns></returns>
    [HttpPut]
    public virtual IActionResult Update(T model)
    {
        var fromDb = _repository.Get(model.Guid);
        if (fromDb == null) return BadRequest("Запись с заданным идентификатором не найдена");


        if (_repository.Update(fromDb))
            return Ok(fromDb);

        return StatusCode(500, "Не удалось обновить запись");
    }
}
