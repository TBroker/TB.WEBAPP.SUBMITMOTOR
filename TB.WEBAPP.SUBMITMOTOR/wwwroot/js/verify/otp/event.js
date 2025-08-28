import * as Doms from "./dom.js";
import * as Alerts from "../../helper/alert.js"

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

        fetch('/VerifyIdentity/ConfirmVerifyOtp', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(async result => {
                if (result.success) {
                    await Alerts.showAlertAndRedirect(new Object({
                        title: 'ยืนยันรหัส OTP',
                        html: `<p>ยืนยันรหัส OTP เรียบร้อยแล้ว</p>`,
                        icon: 'success',
                        url: `/VerifyIdentity/VerifyMobile/${result.data.formData}`,
                        confirmButtonText: 'ตกลง'
                    }));
                    return;
                }

                await Alerts.showAlert(new Object({
                    icon: `warning`,
                    title: `<h5>แจ้งเตือน</h4>`,
                    text: `<span class="text-danger">${result.message}</div>`,
                }));
            })
            .catch(async error => {
                await Alerts.showAlert(new Object({
                    icon: `error`,
                    title: `<h5>พบปัญหา</h4>`,
                    text: `<span class="text-danger">${error}</div>`,
                }));
                button.disabled = false;
                button.innerHTML = '<span>ยืนยันรหัส OTP</span>';
                console.error('Error:', error);
            })
            .finally(async () => {
                button.disabled = false;
                button.innerHTML = '<span>ยืนยันรหัส OTP</span>';
            });
    });
}