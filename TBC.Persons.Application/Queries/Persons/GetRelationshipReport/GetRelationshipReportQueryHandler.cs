using Dapper;
using Shared.Application.Mediatr;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TBC.Persons.Application.Infrastructure;
using TBC.Persons.Domain;
using TBC.Persons.Shared;

namespace TBC.Persons.Application.Queries.Persons.GetRelationshipReport
{
    public class GetRelationshipReportQueryHandler : IQueryHandler<GetRelationshipReportQuery, IEnumerable<RelationshipReportItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;

        public GetRelationshipReportQueryHandler(IUnitOfWork unitOfWork, ApplicationContext applicationContext)
        {
            _unitOfWork = unitOfWork;
            _applicationContext = applicationContext;
        }

        public async Task<IEnumerable<RelationshipReportItem>> Handle(GetRelationshipReportQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _unitOfWork.GetDbConnection())
            {
                var personNameSelection = _applicationContext.Language == Language.Georgian
                    ? "p.Firstname_Georgian + ' ' + p.Lastname_Georgian"
                    : "p.Firstname_English + ' ' + p.Lastname_English";

                var query = @$"select
	                            {personNameSelection} MasterPersonName,
	                            pr.RelationType,
	                            count(*) Count
                            from dbo.PersonRelationships pr
                            inner join dbo.Persons p on p.Id = pr.MasterPersonId
                            group by pr.RelationType, p.Firstname_Georgian, p.Lastname_Georgian, p.Firstname_English, p.Lastname_English";

                var report = await connection.QueryAsync<RelationshipReportItem>(query);

                return report;

            }
        }
    }
}
