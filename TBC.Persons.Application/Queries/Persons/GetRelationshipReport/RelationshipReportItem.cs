using TBC.Persons.Domain.Aggregates.Persons;

namespace TBC.Persons.Application.Queries.Persons.GetRelationshipReport
{
    public class RelationshipReportItem
    {
        public string MasterPersonName { get; set; }

        public RelationType RelationType { get; set; }

        public int Count { get; set; }
    }
}