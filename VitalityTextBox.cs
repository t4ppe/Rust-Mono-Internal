using System;
using System.Drawing;
using System.Windows.Forms;

// Token: 0x02000040 RID: 64
internal class VitalityTextBox : TextBox
{
	// Token: 0x060001E3 RID: 483 RVA: 0x0001379C File Offset: 0x0001199C
	protected override void WndProc(ref Message m)
	{
		int msg = m.Msg;
		if (msg != 15)
		{
			base.WndProc(ref m);
		}
		else
		{
			base.Invalidate();
			base.WndProc(ref m);
			this.CustomPaint();
		}
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x000137DC File Offset: 0x000119DC
	public VitalityTextBox()
	{
		this.Font = new Font("Microsoft Sans Serif", 8f);
		this.ForeColor = Color.Gray;
		this.BackColor = Color.FromArgb(235, 235, 235);
		base.BorderStyle = BorderStyle.FixedSingle;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00013838 File Offset: 0x00011A38
	private void CustomPaint()
	{
		Pen lightGray = Pens.LightGray;
		base.CreateGraphics().DrawLine(lightGray, 0, 0, base.Width, 0);
		base.CreateGraphics().DrawLine(lightGray, 0, base.Height - 1, base.Width, base.Height - 1);
		base.CreateGraphics().DrawLine(lightGray, 0, 0, 0, base.Height - 1);
		base.CreateGraphics().DrawLine(lightGray, base.Width - 1, 0, base.Width - 1, base.Height - 1);
	}
}
