using Lumina.Excel;
using Lumina.Text.ReadOnly;

namespace PvPAnnouncer.Data;


[Sheet("CutsceneText")]
public readonly struct CutsceneText(RawRow row) : IExcelRow<CutsceneText>
{
    public uint RowId => row.RowId;

    public ReadOnlySeString MessageTag => row.ReadStringColumn(0);

    public ReadOnlySeString Dialogue => row.ReadStringColumn(1);
    

    public ExcelPage ExcelPage => row.ExcelPage;
    public uint RowOffset => row.RowOffset;

    static CutsceneText IExcelRow<CutsceneText>.Create(ExcelPage page, uint offset, uint row)
    {
        return new CutsceneText(new RawRow(page, offset, row));
    }
}

