using MiniETicaretAPI.Infrastructure.Operations;

namespace MiniETicaretAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
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

                if (hasFileMethod(pathOrContainerName, newFileName))
                    return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);

                else
                    return newFileName;
            });

            return newFileName;
        }
    }
}
