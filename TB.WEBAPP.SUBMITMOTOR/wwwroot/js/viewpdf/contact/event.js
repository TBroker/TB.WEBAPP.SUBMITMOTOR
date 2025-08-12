export async function initContact() {
    const checkbox = document.getElementById('consentCheckbox');
    const button = document.getElementById('submitConsent');

    checkbox.addEventListener('change', function () {
        button.disabled = !this.checked;
    });
}
export async function initSubmitConfrim() {
    document.getElementById('submitConsent').addEventListener('click', async function () {
        const button = this;
        const form = document.getElementById('confirmConsent');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังตรวจสอบ...';

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        fetch('/ViewPdf/ConFirmContactInstallment', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(result => {
                console.log(result);

                if (result.success) {
                    // Redirect ไปยังหน้า View PDF พร้อมส่ง id
                    const id = 1; // ปรับตามชื่อ field ที่ API ส่งกลับ
                    window.location.href = `/PaymentInstallment/PaymentInstallment`;
                } else {
                    alert(`เกิดข้อผิดพลาด: ${result.message}`);
                }

                //if (data.success) {
                //    messageDiv.className = 'alert alert-success mt-3';
                //    messageDiv.innerHTML = `
                //    <strong>สำเร็จ!</strong> ${data.message}<br>
                //    <small>บันทึกข้อมูลทั้งหมด ${data.data.totalFields} ฟิลด์</small>
                //`;

                //    // ล้างข้อมูลหลังบันทึกสำเร็จ (ถ้าต้องการ)
                //    // form.reset();
                //} else {
                //    messageDiv.className = 'alert alert-danger mt-3';
                //    messageDiv.innerHTML = `<strong>เกิดข้อผิดพลาด!</strong> ${data.message}`;
                //}
                //messageDiv.style.display = 'block';

                //// เลื่อนไปที่ message
                //messageDiv.scrollIntoView({ behavior: 'smooth' });
            })
            .catch(error => {
                //messageDiv.className = 'alert alert-danger mt-3';
                //messageDiv.innerHTML = '<strong>เกิดข้อผิดพลาด!</strong> ไม่สามารถส่งข้อมูลได้';
                //messageDiv.style.display = 'block';
                console.error('Error:', error);
            })
            .finally(async () => {
                button.disabled = false;
                button.innerHTML = '<span>ยืนยันการยินยอม</span>';
            });
    });
}