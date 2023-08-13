using AutoMapper;

namespace UltraPlay_evaluation.Data.Entities
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class XmlSports
    {

        private XmlSportsSport sportField;

        private System.DateTime createDateField;

        /// <remarks/>
        public XmlSportsSport Sport
        {
            get
            {
                return this.sportField;
            }
            set
            {
                this.sportField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime CreateDate
        {
            get
            {
                return this.createDateField;
            }
            set
            {
                this.createDateField = value;
            }
        }

        public List<T> GetLayer<T>(IMapper _mapper) where T : BaseEntity
        {
            return typeof(T) switch
            {
                Type type when type == typeof(Sport) => new List<T>() { _mapper.Map<XmlSportsSport, Sport>(Sport) as T },
                Type type when type == typeof(Event) => Sport.Event.Select(x => _mapper.Map<XmlSportsSportEvent, Event>(x) as T).ToList(),
                Type type when type == typeof(Match) => Sport.Event.SelectMany(x => x.Match).Select(x => _mapper.Map<XmlSportsSportEventMatch, Match>(x) as T).ToList(),
                Type type when type == typeof(Bet) => Sport.Event.SelectMany(x => x.Match).SelectMany(x => x.Bet).Select(x => _mapper.Map<XmlSportsSportEventMatchBet, Bet>(x) as T).ToList(),
                Type type when type == typeof(Odd) => Sport.Event.SelectMany(x => x.Match).SelectMany(x => x.Bet).SelectMany(x => x.Odd).Select(x => _mapper.Map<XmlSportsSportEventMatchBetOdd, Odd>(x) as T).ToList(),
                _ => throw new InvalidOperationException($"Type {typeof(T).Name} is not supported."),
            };
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XmlSportsSport
    {

        private XmlSportsSportEvent[] eventField;

        private string nameField;

        private ushort idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Event")]
        public XmlSportsSportEvent[] Event
        {
            get
            {
                return this.eventField;
            }
            set
            {
                this.eventField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XmlSportsSportEvent
    {

        private XmlSportsSportEventMatch[] matchField;

        private string nameField;

        private int idField;

        private bool isLiveField;

        private ushort categoryIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Match")]
        public XmlSportsSportEventMatch[] Match
        {
            get
            {
                return this.matchField;
            }
            set
            {
                this.matchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsLive
        {
            get
            {
                return this.isLiveField;
            }
            set
            {
                this.isLiveField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort CategoryID
        {
            get
            {
                return this.categoryIDField;
            }
            set
            {
                this.categoryIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XmlSportsSportEventMatch
    {

        private XmlSportsSportEventMatchBet[] betField;

        private string nameField;

        private int idField;

        private System.DateTime startDateField;

        private string matchTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Bet")]
        public XmlSportsSportEventMatchBet[] Bet
        {
            get
            {
                return this.betField;
            }
            set
            {
                this.betField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime StartDate
        {
            get
            {
                return this.startDateField;
            }
            set
            {
                this.startDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string MatchType
        {
            get
            {
                return this.matchTypeField;
            }
            set
            {
                this.matchTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XmlSportsSportEventMatchBet
    {

        private XmlSportsSportEventMatchBetOdd[] oddField;

        private string nameField;

        private int idField;

        private bool isLiveField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Odd")]
        public XmlSportsSportEventMatchBetOdd[] Odd
        {
            get
            {
                return this.oddField;
            }
            set
            {
                this.oddField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool IsLive
        {
            get
            {
                return this.isLiveField;
            }
            set
            {
                this.isLiveField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XmlSportsSportEventMatchBetOdd
    {

        private string nameField;

        private int idField;

        private decimal valueField;

        private decimal specialBetValueField;

        private bool specialBetValueFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal SpecialBetValue
        {
            get
            {
                return this.specialBetValueField;
            }
            set
            {
                this.specialBetValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SpecialBetValueSpecified
        {
            get
            {
                return this.specialBetValueFieldSpecified;
            }
            set
            {
                this.specialBetValueFieldSpecified = value;
            }
        }
    }
}
