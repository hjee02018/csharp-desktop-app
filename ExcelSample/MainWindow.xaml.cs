using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using OfficeOpenXml; // EPPlus 네임스페이스 추가

namespace ExcelSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // 라이선스 컨텍스트 설정
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 또는 Commercial로 설정
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                CheckFileExists = true,
                Filter = "XLSX files (*.xlsx)|*.xlsx", // XLSX 파일 필터 설정
                Title = "Select a XLSX File" // 대화 상자 제목 설정
            };

            // 실행 파일의 경로 가져오기
            string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string filepath = System.IO.Path.GetDirectoryName(path);

            // 기본 열기 경로 설정
            openDialog.InitialDirectory = filepath;

            // 파일 열기 대화 상자 열기
            if (openDialog.ShowDialog() == true)
            {
                // 선택한 파일 경로와 파일 이름을 가져옴
                string selectedFilePath = openDialog.FileName;

                // 선택된 파일 경로를 텍스트 박스 또는 다른 UI 요소에 출력하거나, 필요한 작업 수행
                txtCSVPath.Text = selectedFilePath; // 파일 경로를 텍스트 상자에 출력
                MessageBox.Show($"Selected file path: {selectedFilePath}");

                // CSV 파일을 Excel로 불러오기
                ImportCsvToExcel(selectedFilePath);
            }
        }

        private void ImportCsvToExcel(string csvFilePath)
        {
            // ExcelPackage 사용
            using (ExcelPackage package = new ExcelPackage())
            {
                // "Default"라는 이름의 워크시트 추가
                var defaultWorksheet = package.Workbook.Worksheets.Add("Default");

                // CSV 파일을 읽어와서 Excel에 입력
                string[] csvData = File.ReadAllLines(csvFilePath);
                for (int row = 0; row < csvData.Length; row++)
                {
                    string[] cells = csvData[row].Split(','); // CSV 파일을 쉼표로 분리
                    for (int col = 0; col < cells.Length; col++)
                    {
                        defaultWorksheet.Cells[row + 1, col + 1].Value = cells[col]; // 특정 셀에 데이터 입력
                    }
                }

                // "Default" 시트를 복사하여 새로운 시트 생성 (TRACK당 시트 1개)
                var copiedWorksheet = package.Workbook.Worksheets.Add("Copied_Default");

                // "Default" 시트의 내용을 복사하여 새로운 시트에 붙여넣기
                defaultWorksheet.Cells.Copy(copiedWorksheet.Cells);

                // 복사된 시트의 특정 셀 수정 (예: A1 셀에 "Modified Data" 입력)
                copiedWorksheet.Cells[1, 1].Value = "Modified Data"; // A1 셀 수정

                // Excel 파일 저장
                var excelFilePath = System.IO.Path.ChangeExtension(csvFilePath, ".xlsx");
                File.WriteAllBytes(excelFilePath, package.GetAsByteArray());
                MessageBox.Show($"Excel file saved: {excelFilePath}");
            }
        }



        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}