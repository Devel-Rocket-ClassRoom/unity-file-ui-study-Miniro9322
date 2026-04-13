using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class FileIO : MonoBehaviour
{
    /*
    ### 문제 1. 세이브 파일 관리자

    `Application.persistentDataPath` 아래의 세이브 폴더를 탐색하여 저장된 파일 정보를 출력하고, 특정 파일을 복사/삭제할 수 있는 스크립트를 작성하시오.

    **요구사항**

    1. **세이브 폴더 준비**: `SaveData` 폴더를 만들고, `File.WriteAllText`로 테스트용 파일 3개를 생성할 것
       - `save1.txt`, `save2.txt`, `save3.txt` (내용은 자유)
    2. **파일 목록 출력**: `Directory.GetFiles`로 폴더 내 모든 파일을 조회하고, 각 파일의 이름과 확장자를 출력할 것
    3. **파일 복사**: `save1.txt`를 `save1_backup.txt`로 복사할 것 (`File.Copy`)
    4. **파일 삭제**: `save3.txt`를 삭제할 것(`File.Delete`)
    5. **최종 확인**: 작업 후 파일 목록을 다시 출력하여 결과를 확인할 것

    **예상 출력**

    === 세이브 파일 목록 ===
    - save1.txt (.txt)
    - save2.txt (.txt)
    - save3.txt (.txt)
    save1.txt → save1_backup.txt 복사 완료
    save3.txt 삭제 완료
    === 작업 후 파일 목록 ===
    - save1.txt (.txt)
    - save1_backup.txt (.txt)
    - save2.txt (.txt)
    */
    /*
 	### 문제 2. FileStream으로 간이 파일 암호화

`FileStream`의 `Position`과 바이트 조작을 활용하여 텍스트 파일을 간단히 암호화/복호화하는 스크립트를 작성하시오.

**요구사항**

1. **원본 파일 생성**: `File.WriteAllText`로 `secret.txt`에 영문 메시지를 저장할 것 (예: `"Hello Unity World"`)
2. **암호화**: `FileStream`으로 `secret.txt`를 열어 모든 바이트를 한 바이트씩 읽고, 각 바이트를 특정 키 값(예: `0xAB`)과 XOR 연산한 뒤 `encrypted.dat`에 쓸 것
   - `ReadByte()`로 읽고 `-1`(EOF)이면 종료
   - 각 바이트에 XOR 연산(`^`)을 적용하여 `WriteByte()`로 저장
3. **복호화**: `encrypted.dat`를 읽어 각 바이트에 동일한 키(`0xAB`)로 다시 XOR 연산하여 `decrypted.txt`에 쓸 것
   - XOR은 같은 키로 두 번 적용하면 원본이 복원되는 성질을 이용
4. 원본, 암호화 결과, 복호화 결과를 각각 출력할 것
5. 
**예상 출력**

```
원본: Hello Unity World
암호화 완료 (파일 크기: 17 bytes)
복호화 완료
복호화 결과: Hello Unity World
원본과 일치: True
```
 */
    private void Start()
    {
        //1. **세이브 폴더 준비**: `SaveData` 폴더를 만들고, `File.WriteAllText`로 테스트용 파일 3개를 생성할 것
        string Folder = Path.Combine(Application.persistentDataPath, "SaveData");

        if (!Directory.Exists(Folder))
        {
            Directory.CreateDirectory(Folder);
        }

        string save1 = Path.Combine(Application.persistentDataPath, "SaveData", "save1.txt");
        string save2 = Path.Combine(Application.persistentDataPath, "SaveData", "save2.txt");
        string save3 = Path.Combine(Application.persistentDataPath, "SaveData", "save3.txt");

        string save1Data = "save1.txt\nㅁㄴㅇㄹ";
        string save2Data = "save2.txt\nㅁㄴㅇㄹ";
        string save3Data = "save3.txt\nㅁㄴㅇㄹ";

        File.WriteAllText(save1, save1Data);
        File.WriteAllText(save2, save2Data);
        File.WriteAllText(save3, save3Data);

        //2. **파일 목록 출력**: `Directory.GetFiles`로 폴더 내 모든 파일을 조회하고, 각 파일의 이름과 확장자를 출력할 것
        string dir = Path.Combine(Application.persistentDataPath, "SaveData");
        string backup = Path.Combine(Application.persistentDataPath, "SaveData", "save1_backup.txt");
        if (File.Exists(backup) == true)
        {
            File.Delete(backup);
        }

        Debug.Log("=== 세이브 파일 목록 ===");
        var files = Directory.GetFiles(dir);
        foreach(var file in files)
        {
            Debug.Log($" - {Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }

        //3. **파일 복사**: `save1.txt`를 `save1_backup.txt`로 복사할 것 (`File.Copy`)
        File.Copy(save1, backup);
        Debug.Log("save1.txt → save1_backup.txt 복사 완료");

        //4. **파일 삭제**: `save3.txt`를 삭제할 것(`File.Delete`)
        File.Delete(save3);
        Debug.Log("save3.txt 삭제 완료");

        //5. **최종 확인**: 작업 후 파일 목록을 다시 출력하여 결과를 확인할 것
        files = Directory.GetFiles(dir);
        Debug.Log("=== 작업 후 파일 목록 ===");
        foreach (var file in files)
        {
            Debug.Log($" - {Path.GetFileName(file)} ({Path.GetExtension(file)})");
        }

        //1. **원본 파일 생성**: `File.WriteAllText`로 `secret.txt`에 영문 메시지를 저장할 것 (예: `"Hello Unity World"`)
        string secret = Path.Combine(Application.persistentDataPath, "secret.txt");
        string message = "Hello Unity World";

        File.WriteAllText(secret, message);

        var origin = File.ReadAllText(secret);

        Debug.Log($"원본: {origin}");

        /*
        2. * *암호화 * *: `FileStream`으로 `secret.txt`를 열어 모든 바이트를 한 바이트씩 읽고, 각 바이트를 특정 키 값(예: `0xAB`)과 XOR 연산한 뒤 `encrypted.dat`에 쓸 것
        - `ReadByte()`로 읽고 `-1`(EOF)이면 종료
        - 각 바이트에 XOR 연산(`^`)을 적용하여 `WriteByte()`로 저장
        */

        string encrypted = Path.Combine(Application.persistentDataPath, "encrypted.dat");

        byte key = 0xAB;

        using(FileStream fs = new FileStream(secret, FileMode.Open))
        {
            using(FileStream fs2 = new FileStream(encrypted, FileMode.Create))
            {
                while(true)
                {
                    var data = fs.ReadByte();
                    if(data == -1)
                    {
                        break;
                    }

                    var temp = data ^ key;

                    fs2.WriteByte((byte)temp);
                }
                Debug.Log($"암호화 완료: (파일 크기: {fs2.Length} bytes");
            }
        }

        /*
        3. * *복호화 * *: `encrypted.dat`를 읽어 각 바이트에 동일한 키(`0xAB`)로 다시 XOR 연산하여 `decrypted.txt`에 쓸 것
        - XOR은 같은 키로 두 번 적용하면 원본이 복원되는 성질을 이용
        */
        string decrypted = Path.Combine(Application.persistentDataPath, "decrypted.dat");

        using (FileStream fs = new FileStream(encrypted, FileMode.Open))
        {
            using (FileStream fs2 = new FileStream(decrypted, FileMode.Create))
            {
                while (true)
                {
                    var data = fs.ReadByte();
                    if (data == -1)
                    {
                        break;
                    }

                    var temp = data ^ key;

                    fs2.WriteByte((byte)temp);
                }
                Debug.Log("복호화 완료");
            }
        }

        //4. 원본, 암호화 결과, 복호화 결과를 각각 출력할 것
        var decrypt = File.ReadAllText(decrypted);
        Debug.Log($"복호화 결과: {decrypt}");
        Debug.Log($"원본과 일치: {decrypt == origin}");

        //3
        string settings = Path.Combine(Application.persistentDataPath, "settings.cfg");

        Dictionary<string, string> keyValue = new Dictionary<string, string>();
        using (StreamReader reader = File.OpenText(settings))
        {
            string line;
            while((line = reader.ReadLine()) != null)
            {
                var temp = line.Split("=");
                keyValue[temp[0]] = temp[1];
            }
            Debug.Log($"설정 로드 완료 (항목 {keyValue.Count}개");
        }

        Debug.Log("--- 변경 전 ---");
        Debug.Log($"bgm_volume = {keyValue["bgm_volume"]}");
        Debug.Log($"language = {keyValue["language"]}");

        keyValue["bgm_volume"] = "50";
        keyValue["language"] = "en";

        Debug.Log("--- 변경 후 저장 ---");
        Debug.Log($"bgm_volume = {keyValue["bgm_volume"]}");
        Debug.Log($"language = {keyValue["language"]}");

        using (StreamWriter writer = File.CreateText(settings))
        {
            foreach(var text in keyValue)
            {
                writer.WriteLine($"{text.Key}={text.Value}");
            }
        }

        Debug.Log("--- 최종 파일 내용 ---");
        using (StreamReader reader = File.OpenText(settings))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Debug.Log(line);
            }
        }
    }
}

/*
 ### 문제 3. 간이 키-값 설정 파일 시스템

게임 설정을 `키=값` 형식의 텍스트 파일로 관리하는 시스템을 구현하시오. 설정 값을 수정한 뒤 파일에 다시 저장할 수 있어야 한다.

**요구사항**

- 아래 형식의 설정 파일(`settings.cfg`)을 생성할 것
  ```
  master_volume=80
  bgm_volume=70
  sfx_volume=90
  language=kr
  show_damage=true
  ```
- `StreamReader`로 파일을 읽어 `Dictionary<string, string>`에 파싱하여 저장할 것
  - `=`를 기준으로 키와 값을 분리
- Dictionary에서 `bgm_volume`을 `50`으로, `language`를 `en`으로 변경할 것
- 변경된 Dictionary 내용을 다시 `StreamWriter`로 파일에 덮어쓸 것
- 최종 파일 내용을 `File.ReadAllText`로 읽어 출력하여 변경이 반영되었는지 확인할 것

**예상 출력**

```
설정 로드 완료 (항목 5개)
--- 변경 전 ---
bgm_volume = 70
language = kr
--- 변경 후 저장 ---
bgm_volume = 50
language = en
--- 최종 파일 내용 ---
master_volume=80
bgm_volume=50
sfx_volume=90
language=en
show_damage=true
```
*/
