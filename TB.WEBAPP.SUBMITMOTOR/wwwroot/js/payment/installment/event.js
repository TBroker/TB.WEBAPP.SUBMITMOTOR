import * as Alerts from "../../helper/alert.js"

export async function initSubmitConfrim() {
    document.getElementById('paymentButton').addEventListener('click', async function () {
        const button = this;
        const form = document.getElementById('paymentInstallment');

        // แสดง loading
        button.disabled = true;
        button.innerHTML = '<span class="spinner-border spinner-border-sm"></span> กำลังตรวจสอบ...';

        // รวบรวมข้อมูลทั้งหมดจาก form
        const formData = new FormData(form);

        fetch('/PaymentInstallment/ConfirmPaymentInstallment', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(async result => {
                console.log(result);
                if (result.success) {
                    await Alerts.showAlertAndRedirect(new Object({
                        title: 'แจ้งเตือน',
                        html: `<p>${result.message}</p>`,
                        icon: 'question',
                        url: `/PaymentKBank/PaymentQRCodeInstallment`,
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
                button.innerHTML = '<span>ชำระเงิน</span>';
                console.error('Error:', error);
            })
            .finally(async () => {
                button.disabled = false;
                button.innerHTML = '<span>ชำระเงิน</span>';
            });
    });
}