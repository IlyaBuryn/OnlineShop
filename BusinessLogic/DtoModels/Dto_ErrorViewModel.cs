namespace BusinessLogic.DtoModels
{
    public class Dto_ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
