using FileImportLibrary;

namespace EduERPApi.BusinessLayer
{

    public abstract class Parser<TResult>
    {
        private PathReader _PathReader;
        public Parser(PathReader pathReader)
        {
            _PathReader = pathReader;
        }   
        public abstract string Parse()
        {

        }
    }

    public abstract class PathReader
    {
        public abstract string ReadPath();
    }

   
}
