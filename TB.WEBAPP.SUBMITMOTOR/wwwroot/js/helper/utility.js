/**
 * แปลงค่าตัวเลขให้เป็นข้อความที่อยู่ในรูปแบบ "จำนวนเงิน" สำหรับการแสดงผล
 * เช่น 123456.789 → "123,456.79"
 * ใช้สำหรับการแสดงผลใน UI
 * @param {number|string} amount - ตัวเลขที่ต้องการแปลง
 * @param {boolean} showDecimal - แสดงจุดทศนิยมหรือไม่ (2 ตำแหน่ง)
 * @returns {string} - ข้อความที่ถูกจัดรูปแบบแล้ว เช่น "12,345.00"
 * @example
 * formatToDisplayCurrency(123456.789); // "123,456.79"
 **/
export function formatToDisplayCurrency(amount, showDecimal = true) {
    if (amount == null || isNaN(amount)) {
        return "0.00"; // คืนค่าเป็น 0.00 หากไม่ใช่ตัวเลข
    }
    const numericAmount = Math.abs(parseFloat(amount)); // แปลงให้เป็นเลขบวกแน่นอน
    if (showDecimal) {
        // กรณีแสดงทศนิยม: ปัดทศนิยม 2 ตำแหน่งและใส่ comma คั่นหลักพัน
        return numericAmount
            .toFixed(2)
            .replace(/(\d)(?=(\d{3})+\.)/g, "$1,");
    } else {
        // กรณีไม่แสดงทศนิยม: ปัดเศษลงและใส่ comma
        return Math.floor(numericAmount)
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }
}

/**
 * แปลงค่าที่เป็นตัวเลข (หรือสตริงที่เป็นตัวเลข) ให้เป็นตัวเลขอารบิก
 * รองรับเลขไทย (๐-๙) และอารบิก (0-9)
 * * @param {any} input - ค่าที่ต้องการแปลงเป็นตัวเลข
 * * @param {any} defaultValue - ค่าที่จะคืนกลับหากไม่สามารถแปลงได้ (default = NaN)
 * * @returns {number} - ค่าที่แปลงแล้วเป็นตัวเลขอารบิก หรือ defaultValue หากไม่สามารถแปลงได้
 * * @example
 * * convertToNumber("๓๔๕") // 345
 */
export function convertToNumber(input, defaultValue = NaN) {
    if (input == null) return defaultValue;

    // แปลงเลขไทย → อารบิก
    const thaiDigits = '๐๑๒๓๔๕๖๗๘๙';
    const arabicDigits = '0123456789';
    let normalized = String(input).trim().replace(/[\s,]/g, '');

    normalized = normalized.replace(/[๐-๙]/g, (digit) => {
        return arabicDigits[thaiDigits.indexOf(digit)];
    });

    const number = parseFloat(normalized);

    return isNaN(number) ? defaultValue : number;
}

/**
 * แยกหมายเลขทะเบียนรถออกเป็นส่วนต่างๆ
 * เช่น "กข 1234" → ["กข", "1234", ""]
 * * @param {string} value - หมายเลขทะเบียนรถที่ต้องการแยก
 * * @returns {Array} - อาร์เรย์ที่ประกอบด้วยส่วนต่างๆ ของหมายเลขทะเบียน
 * * @example
 * * splitVehicle("กข 1234") // ["กข", "1234", ""]
 */
export function splitVehicleNumber(value) {
    // ตรวจสอบว่า input เป็น string หรือไม่
    if (typeof value !== 'string') {
        throw new TypeError("Input must be a string");
    }

    // ลบช่องว่างและขีดทั้งหมดก่อนแยก
    const cleaned = value.replace(/[\s\\-]/g, ''); // ลบช่องว่างและขีดทั้งหมด
    if (cleaned === "") {
        return ["", "", ""]; // ถ้าไม่มีข้อมูลเลย
    }

    // pattern:
    // 1. หมวดอักษรไทย 1-3 ตัว (กข)
    // 2. ตัวเลข 1-4 หลัก
    // 3. จังหวัด (ตัวอักษรไทยต่อท้าย ยาวได้ เช่น กทม, กรุงเทพมหานคร)
    const pattern = /^(\d?[ก-ฮ]{1,3})(\d{1,4})([ก-๙]{2,})?$/;
    const match = RegExp(pattern).exec(cleaned);

    // ถ้าไม่ตรงกับ pattern จะคืนค่าเป็น null
    if (!match) {
        return ["", "", ""]; // ถ้าไม่ตรง pattern
    }
    // match[0] = ทั้งหมด, match[1] = อักษร, match[2] = เลข, match[3] = หมวดต่อท้าย (ถ้ามี)
    return [match[1], match[2], match[3] || ""];
}

/**
 * คำนวณอายุจากวันเกิด (ปีเต็ม) โดยใช้ dayjs
 * รองรับรูปแบบวันที่ต่างๆ เช่น "YYYY-MM-DD", "DD/MM/YYYY", "DD-MM-YYYY" และอื่นๆ
 * @param {string|Date|dayjs.Dayjs} birthDate - วันเกิด
 * @returns {number} อายุเป็นจำนวนปี
 * @example
 * calculateAge("1990-01-01") // 33 (ถ้าเป็นปี 2023)
 */
export function calculateAge(birthDate) {
    const today = dayjs();
    const birth = dayjs(birthDate);

    if (!birth.isValid()) return 0;

    let age = today.year() - birth.year();

    // ถ้ายังไม่ถึงวันเกิดปีนี้ ให้ลบออก 1 ปี
    if (today.month() < birth.month() || (today.month() === birth.month() && today.date() < birth.date())) {
        age--;
    }

    return age;
}

/**
 * เพิ่ม 1 ปีจากวันที่ที่รับมา (รองรับปีอธิกสุรทิน 29 ก.พ.)
 * @param {Date} date - วันที่เริ่มต้น
 * @returns {Date} วันที่ใหม่หลังเพิ่ม 1 ปี
 * @example
 * addOneYear(new Date("2020-02-29")) // 2021-02-28
 */
export function addOneYear(date) {
    const dayjsDate = dayjs(date);
    if (!dayjsDate.isValid()) return null;

    // ถ้าวันที่เป็น 29 ก.พ. ให้เลื่อนไปเป็น 28 ก.พ. ของปีถัดไป
    if (dayjsDate.date() === 29 && dayjsDate.month() === 1) {
        return dayjsDate.subtract(1, "day").add(1, "year").toDate();
    }

    return dayjsDate.add(1, "year").toDate();
}

/**
 * @param {any} date
 * @param {any} format
 * @returns
 */
export function formatDate(date, format = 'YYYY-MM-DD') {
    const d = new Date(date);
    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const day = String(d.getDate()).padStart(2, '0');
    const hours = String(d.getHours()).padStart(2, '0');
    const minutes = String(d.getMinutes()).padStart(2, '0');
    const seconds = String(d.getSeconds()).padStart(2, '0');

    switch (format) {
        case 'YYYY-MM-DD':
            return `${year}-${month}-${day}`;
        case 'DD/MM/YYYY':
            return `${day}/${month}/${year}`;
        case 'YYYY-MM-DD HH:mm:ss':
            return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
        case 'DD MMM YYYY':
            return `${day} ${d.toLocaleString('default', { month: 'short' })} ${year}`;
        default:
            return d.toString(); // คืนค่าแบบเต็มถ้า format ไม่ตรง
    }
}


/**
    * ตรวจสอบว่า input เป็นตัวเลขที่ใช้ได้หรือไม่
    * @param {any} input - ค่าที่ต้องการตรวจสอบ
    * @returns {boolean}
    * @example
    * isValidNumber(123) // true
 */
document.querySelectorAll('.numberOnly').forEach(input => {
    input.addEventListener('keypress', function (e) {
        const charCode = e.which || e.keyCode;
        const charStr = String.fromCharCode(charCode);
        // ตรวจสอบว่าเป็นตัวเลขหรือไม่
        if (/\D/.test(charStr)) {
            e.preventDefault();
        }
    });
});

/**
 * แปลง input ให้เป็นตัวพิมพ์ใหญ่
 * รองรับการป้องกันการกด space หรือ ก-๙
 * * @example
 * * <input type="text" class="toUpperCase" />
 */
document.querySelectorAll('.toUpperCase').forEach(input => {
    // แปลงเป็นตัวพิมพ์ใหญ่ตอนพิมพ์ (keyup)
    input.addEventListener('keyup', (e) => {
        e.target.value = e.target.value.toUpperCase();
    });

    // ป้องกันกด space หรือ ก-๙ (keydown)
    input.addEventListener('keydown', (e) => {
        if (e.key === ' ' || /[ก-๙]/.test(e.key)) {
            e.preventDefault();
        }
    });
});