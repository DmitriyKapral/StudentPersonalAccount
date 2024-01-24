using Microsoft.AspNetCore.Mvc;

namespace StudentPersonalAccount.Interfaces;

public interface IBaseCRUDController<T> where T : IGuidKey
{
    public IActionResult Get();
    public IActionResult ListEntitiesPiece(int limit, int numberSkip);
    public IActionResult Get(Guid guid);
    public IActionResult Add(T model);
    public IActionResult AddRange(List<T> models);
    public IActionResult RemoveByGuid(Guid guid);
    public IActionResult RemoveRangeByGuid(List<Guid> guids);
    public IActionResult Update(T model);
}

