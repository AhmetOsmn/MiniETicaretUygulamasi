using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MiniETicaretAPI.Application.Services;
using MiniETicaretAPI.Infrastructure.Operations;

namespace MiniETicaretAPI.Infrastructure.Services
{
    public class FileService : IFileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }


        // metot ilk calistiginda karakter temizlemeleri yapilacak.
        // sonraki recursive calismalarinda ise uygun dosya adi verilecek.
        async Task<string> FileRenameAsync(string path, string fileName, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = string.Empty;

                // ilk defa calisti ise temizleme yap
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                }
                // ilk degil ise uygun ismi bul
                else
                {
                    newFileName = fileName;
                    int index1 = newFileName.IndexOf("-");

                    // eger dosyanin adinda '-' yok ise sadece 1 adet ayni isimli dosya var demektir. 
                    // bu nedenle dosya adinin sonuna '-2' ekleyerek isimlendirme yapilacak.
                    if (index1 == -1)
                    {
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    }

                    // eger dosya adinda '-' var ise '-' isaretinden sonraki sayinin bir fazlasini kullanarak yeni dosya ismi olusturulmali.
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = index1;
                            index1 = newFileName.IndexOf("-", index1 + 1);
                            if (index1 == -1)
                            {
                                index1 = lastIndex;
                                break;
                            }
                        }

                        int index2 = newFileName.IndexOf(".");
                        int fileNoCharCount = index2 - index1 - 1;
                        string fileNo = newFileName.Substring(index1 + 1, fileNoCharCount);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(index1 + 1, fileNoCharCount).Insert(index1 + 1, _fileNo.ToString());
                        }
                        else
                        {
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                        }
                    }
                }

                if (File.Exists($"{path}\\{newFileName}"))
                    return await FileRenameAsync(path, newFileName, false);

                else
                    return newFileName;
            });

            return newFileName;
        }

        public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();
            List<bool> results = new();

            foreach (IFormFile file in files)
            {
                string fileNewName = await FileRenameAsync(uploadPath, file.FileName);

                bool result = await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);
                datas.Add((fileNewName, $"{path}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(x => x.Equals(true)))
            {
                return datas;
            }

            //todo Eğer yukarıdaki if geçerli değil ise, burada dosyaların sunucuya yüklenirken bir hata alındığına dair uyarıcı bir exception oluşturulup fırlatırlması gerekiyor. 

            return null;
        }
    }
}
