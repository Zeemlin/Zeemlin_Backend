using Newtonsoft.Json;
using Zeemlin.Domain.Commons;
using Zeemlin.Service.Commons.Helpers;
using Zeemlin.Service.Configurations;
using Zeemlin.Service.Exceptions;

namespace Zeemlin.Service.Commons.Extentions;

public static class CollectionExtention
{
    public static IQueryable<TEntity> ToPagedList<TEntity>(this IQueryable<TEntity> source, PaginationParams @params)
            where TEntity : Auditable
    {

        var metaData = new PaginationMetaData(source.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);
        if (HttpContextHelper.ResponseHeaders != null)
        {
            if (HttpContextHelper.ResponseHeaders.ContainsKey("X-Pagination"))
                HttpContextHelper.ResponseHeaders.Remove("X-Pagination");

            HttpContextHelper.ResponseHeaders.Add("X-Pagination", json);
        }

        return @params.PageIndex > 0 && @params.PageSize > 0 ?
        source
            .OrderBy(s => s.Id)
            .Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : throw new ZeemlinException(400, "Please, enter valid numbers");
    }
}
