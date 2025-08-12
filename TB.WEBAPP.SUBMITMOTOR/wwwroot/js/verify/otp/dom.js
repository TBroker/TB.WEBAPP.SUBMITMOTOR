import * as Events from "./event.js";

export async function initForm() {
    // ✅ โหลดข้อมูล + bind event
    await initStartCountdown();

    // ✅ โหลด DOM ก่อน
    await Events.initSubmitConfrim();
    await Events.initResendOtp();
}

let otpTimer = null; // เก็บ timer reference
export async function initStartCountdown() {
    let timeLeft = 300; // 5 นาที = 300 วินาที
    const countdownEl = document.getElementById('countdown');
    const verifyOtpButton = document.getElementById('verifyOtpButton');
    const resendOtpButton = document.getElementById('resendOtpButton');

    // หยุด timer เก่าก่อน (ถ้ามี) เพื่อป้องกันการทำงานซ้ำ
    if (otpTimer) {
        clearInterval(otpTimer);
    }

    // รีเซ็ตสถานะปุ่ม
    verifyOtpButton.disabled = false;
    countdownEl.classList.remove("text-muted");
    countdownEl.classList.add("text-danger");

    // เริ่ม timer ใหม่
    otpTimer = setInterval(() => {
        const minutes = Math.floor(timeLeft / 60);
        const seconds = timeLeft % 60;
        countdownEl.textContent = `เหลือเวลา: ${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`;

        // ปิดปุ่ม resend ใน 60 วินาทีแรก (4 นาที)
        if (timeLeft >= 240) {
            resendOtpButton.disabled = true;
        } else {
            resendOtpButton.disabled = false;
        }

        // เปลี่ยนสีข้อความเมื่อเหลือเวลาไม่ถึง 1 นาที
        if (timeLeft <= 60) {
            countdownEl.classList.remove("text-primary");
            countdownEl.classList.add("text-danger");
        }

        // เมื่อหมดเวลา
        if (timeLeft <= 0) {
            clearInterval(otpTimer);
            otpTimer = null;
            countdownEl.textContent = "รหัสหมดอายุแล้ว กรุณาขอรหัสใหม่";
            countdownEl.classList.remove("text-danger");
            countdownEl.classList.add("text-muted");
            verifyOtpButton.disabled = true;
            resendOtpButton.disabled = false;
        }

        timeLeft--;
    }, 1000);
}

// ฟังก์ชันสำหรับหยุด timer (ถ้าต้องการใช้ที่อื่น)
export function stopCountdown() {
    if (otpTimer) {
        clearInterval(otpTimer);
        otpTimer = null;
    }
}

// ฟังก์ชันสำหรับรีสตาร์ท countdown เมื่อมีการ resend
export function restartCountdown() {
    // หยุด timer เก่าก่อน
    if (otpTimer) {
        clearInterval(otpTimer);
        otpTimer = null;
    }

    // เริ่มใหม่
    initStartCountdown();
}