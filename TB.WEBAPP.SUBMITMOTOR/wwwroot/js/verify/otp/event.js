import * as Doms from "./dom.js";

export async function initResendOtp() {
    document.getElementById('resendOtpButton').addEventListener('click', async function () {
        const button = this;
        const form = document.getElementById('resendOtpForm');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังส่งรหัสใหม่...';

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        fetch('/VerifyIdentity/ResendVerifyOtp', {
            method: 'POST',
            body: formData, // ถ้าต้องการส่งข้อมูลเพิ่มเติม สามารถใส่ที่นี่
        })
            .then(response => response.json())
            .then(result => {
                console.log(result);
                const otpReferenceSpan = document.getElementById('otpReferenceSpan');
                otpReferenceSpan.textContent = result.data.otpRef;

                Doms.restartCountdown();
            })
            .catch(error => {
                console.error('Error:', error);
            })
            .finally(async () => {
                button.disabled = false;
                button.innerHTML = '<span>คลิกที่นี่เพื่อขอรหัสใหม่</span>';
            });
    });
}

export async function initSubmitConfrim() {
    document.getElementById('verifyOtpButton').addEventListener('click', async function () {
        const button = this;
        const form = document.getElementById('verifyOtpForm');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังตรวจสอบ...';

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        fetch('/VerifyIdentity/ConFirmVerifyOtp', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(result => {
                if (result.success) {                    
                    window.location.href = `/VerifyIdentity/VerifyMobile/${result.data.formData}`;
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
                button.innerHTML = '<span>ยืนยันรหัส OTP</span>';
            });
    });
}