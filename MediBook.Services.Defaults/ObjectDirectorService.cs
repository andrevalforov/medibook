//using System.Collections.Generic;
//using ExtCore.Data.Abstractions;
//using Newtonsoft.Json;
//using Platformus;
//using Platformus.Core.Services.Abstractions;
//using Platformus.Website.Data.Entities;

//namespace MediBook.Services.Defaults
//{
//  public class ObjectDirectorService
//  {
//    private readonly IStorage storage;
//    private readonly IClassRepository classRepository;
//    private readonly IMemberRepository memberRepository;
//    private readonly IDataTypeRepository dataTypeRepository;
//    private readonly IPropertyRepository propertyRepository;

//    public ObjectDirectorService(IStorage storage, ICultureManager cultureManager)
//    {
//      this.storage = storage;
//      this.classRepository = this.storage.GetRepository<IClassRepository>();
//      this.memberRepository = this.storage.GetRepository<IMemberRepository>();
//      this.dataTypeRepository = this.storage.GetRepository<IDataTypeRepository>();
//      this.propertyRepository = this.storage.GetRepository<IPropertyRepository>();
//    }

//    public void ConstructObject(ObjectBuilderBase objectBuilder, Object @object)
//    {
//      objectBuilder.BuildId(@object.Id);

//      Class @class = this.classRepository.WithKey(@object.ClassId);

//      foreach (Member member in this.memberRepository.FilteredByClassIdInlcudingParent(@class.Id))
//      {
//        if (member.PropertyDataTypeId != null)
//        {
//          DataType dataType = this.dataTypeRepository.WithKey((int)member.PropertyDataTypeId);

//          this.ConstructProperty(objectBuilder, @object, member, dataType);
//        }

//        else if (member.RelationClassId != null)
//          this.ConstructRelation(objectBuilder, @object, member);
//      }
//    }
//    public void ConstructObject(ObjectBuilderBase objectBuilder, SerializedObject serializedObject)
//    {
//      foreach (SerializedProperty serializedProperty in JsonConvert.DeserializeObject<IEnumerable<SerializedProperty>>(serializedObject.SerializedProperties))
//      {
//        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataTypes.Integer)
//          objectBuilder.BuildIntegerProperty(serializedProperty.Member.Code, serializedProperty.IntegerValue);

//        else if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataTypes.Decimal)
//          objectBuilder.BuildDecimalProperty(serializedProperty.Member.Code, serializedProperty.DecimalValue);

//        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataTypes.String)
//        {
//          Member member = this.memberRepository.WithClassIdAndCodeInlcudingParent(serializedObject.ClassId, serializedProperty.Member.Code);
//          Property property = this.propertyRepository.WithObjectIdAndMemberId(serializedObject.ObjectId, member.Id);

//          if (member.IsPropertyLocalizable == true)
//            objectBuilder.BuildStringProperty(serializedProperty.Member.Code, this.GetLocalizationValuesByCultureCodes(property));

//          else objectBuilder.BuildStringProperty(serializedProperty.Member.Code, property.StringValue.GetLocalizationValue());
//        }

//        if (serializedProperty.Member.PropertyDataTypeStorageDataType == StorageDataTypes.DateTime)
//          objectBuilder.BuildDateTimeProperty(serializedProperty.Member.Code, serializedProperty.DateTimeValue);
//      }
//    }

//    private void ConstructProperty(ObjectBuilderBase objectBuilder, Object @object, Member member, DataType dataType)
//    {
//      Property property = this.propertyRepository.WithObjectIdAndMemberId(@object.Id, member.Id);

//      if (dataType.StorageDataType == StorageDataTypes.Integer)
//        objectBuilder.BuildIntegerProperty(member.Code, property.IntegerValue);

//      else if (dataType.StorageDataType == StorageDataTypes.Decimal)
//        objectBuilder.BuildDecimalProperty(member.Code, property.DecimalValue);

//      if (dataType.StorageDataType == StorageDataTypes.String)
//      {
//        if (member.IsPropertyLocalizable == true)
//          objectBuilder.BuildStringProperty(member.Code, this.GetLocalizationValuesByCultureCodes(property));

//        else objectBuilder.BuildStringProperty(member.Code, property.StringValue.GetLocalizationValue());
//      }

//      if (dataType.StorageDataType == StorageDataTypes.DateTime)
//        objectBuilder.BuildDateTimeProperty(member.Code, property.DateTimeValue);
//    }

//    private void ConstructRelation(ObjectBuilderBase objectBuilder, Object @object, Member member)
//    {
//      // TODO: implement relations processing
//    }

//    //private IDictionary<string, string> GetLocalizationValuesByCultureCodes(Property property)
//    //{
//    //  Dictionary<string, string> localizationValuesByCultureCodes = new Dictionary<string, string>();

//    //  foreach (Culture culture in this.cultureManager.GetNotNeutralCultures())
//    //    localizationValuesByCultureCodes.Add(
//    //      culture.Code,
//    //      this.localizationRepository.WithDictionaryIdAndCultureId((int)property.StringValueId, culture.Id)?.Value
//    //    );

//    //  return localizationValuesByCultureCodes;
//    //}
//  }
//}
