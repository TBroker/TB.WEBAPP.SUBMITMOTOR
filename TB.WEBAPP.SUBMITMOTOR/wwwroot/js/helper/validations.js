/**
 * ตรวจสอบว่าเป็นตัวเลขที่ถูกต้องหรือไม่
 * @param {number|string} value - ค่าที่ต้องการตรวจสอบ
 * @return {boolean} - คืนค่า true หากเป็นตัวเลขที่ถูกต้อง, false หากไม่ใช่
 * @example
 * isValidNumber(123); // true
 * isValidNumber('123.45'); // true
 * isValidNumber('abc'); // false
*/
export function isValidateNumber(value) {
    // ตรวจสอบว่าเป็น undefined หรือ null
    if (value === undefined || value === null) {
        return false; // ไม่ใช่ตัวเลขที่ถูกต้อง
    }

    // ตรวจสอบว่าเป็นตัวเลขหรือไม่
    if (typeof value === 'number') {
        return !isNaN(value) && isFinite(value);
    }
    // ถ้าเป็น string ให้แปลงเป็นตัวเลขก่อนตรวจสอบ
    if (typeof value === 'string') {
        const num = parseFloat(value);
        return !isNaN(num) && isFinite(num);
    }
    return false; // ถ้าไม่ใช่ทั้งสองกรณี
}

/**
 * ตรวจสอบว่าเลขบัตรประชาชนไทยถูกต้องหรือไม่
 * @param {string} id - เลขบัตรประชาชนไทย 13 หลัก
 * @return {boolean} - คืนค่า true หากเลขบัตรประชาชนถูกต้อง, false หากไม่ถูกต้อง
 * @example
 * validateThaiID("1234567890123"); // true
 * validateThaiID("1234567890124"); // false
 * */
export function isValidateThaiID(id) {
    // ตรวจสอบว่าเป็นตัวเลขและมีความยาว 13 หลัก
    if (!/^\d{13}$/.test(id)) {
        return false;
    }

    // แปลงเป็น array ของตัวเลข
    const digits = id.split('').map(Number);

    // คำนวณ checksum ตามอัลกอริทึมของเลขบัตรประชาชนไทย
    let sum = 0;
    for (let i = 0; i < 12; i++) {
        sum += digits[i] * (13 - i);
    }

    // คำนวณเลขตรวจสอบ
    const checkDigit = (11 - (sum % 11)) % 10;

    // เปรียบเทียบกับหลักสุดท้าย
    return checkDigit === digits[12];
}

/**
 * ตรวจสอบขนาดไฟล์ที่อัปโหลดแบบ asynchronous
 * @param {HTMLInputElement} inputElement - element input ชนิด type="file"
 * @param {number} maxSizeMB - ขนาดไฟล์สูงสุดที่อนุญาต (ค่าเริ่มต้นคือ 10 MB)
 * @returns {Promise<boolean>} - คืนค่า true หากไฟล์มีขนาดไม่เกินที่กำหนด, false หากเกิน
 * @description
 * ฟังก์ชันนี้จะตรวจสอบขนาดไฟล์ที่ผู้ใช้เลือกจาก input element ว่ามีขนาดไม่เกินที่กำหนด (ค่าเริ่มต้นคือ 10 MB)
 * หากไฟล์มีขนาดเกิน จะคืนค่า false และสามารถแสดง Alert แจ้งเตือนได้ตามต้องการ
 * ตัวอย่างการใช้งาน:
 * ```javascript
 * const inputElement = document.getElementById('fileInput');
 * const isValid = await validateFileSizeAsync(inputElement, 5); // ตรวจสอบไฟล์ไม่เกิน 5 MB
 */
export async function isValidateFileSizeAsync(inputElement, maxSizeMB = 10) {
    // ดึงไฟล์แรกจาก input
    const selectedFile = inputElement.files[0];

    // ตรวจสอบว่าไฟล์มีอยู่หรือไม่
    if (!selectedFile) return false;

    // แปลงขนาดไฟล์เป็น MB และปัดเศษทศนิยม 2 ตำแหน่ง
    const fileSizeInMB = (selectedFile.size / 1024 / 1024).toFixed(2);

    // คืนค่า true หากไฟล์มีขนาดไม่เกินที่กำหนด
    return (fileSizeInMB < maxSizeMB);
}

/**
 * ตรวจสอบประเภทไฟล์ที่อัปโหลดแบบ asynchronous
 * @param {HTMLInputElement} inputElement - element input ชนิด type="file"
 * @param {string[]} allowedMimeTypes - รายการ MIME types ที่อนุญาต (ค่าเริ่มต้นคือ ['image/jpeg', 'image/png', 'application/pdf'])
 * @returns {Promise<boolean>} - คืนค่า true หากไฟล์มี MIME type ที่อนุญาต, false หากไม่อนุญาต
 * @description
 * ฟังก์ชันนี้จะตรวจสอบประเภทไฟล์ที่ผู้ใช้เลือกจาก input element ว่าเป็น MIME type ที่อนุญาตหรือไม่
 * หากไฟล์มี MIME type ที่ไม่อนุญาต จะคืนค่า false และสามารถแสดง Alert แจ้งเตือนได้ตามต้องการ
 * ตัวอย่างการใช้งาน:
 * ```javascript
 * const inputElement = document.getElementById('fileInput');
 * const isValid = await validateFileExtensionAsync(inputElement, ['image/jpeg', 'image/png']); // ตรวจสอบไฟล์เป็น JPEG หรือ PNG
 */
export async function isValidateFileExtensionAsync(inputElement, allowedMimeTypes = ['image/jpeg', 'image/png', 'application/pdf']) {
    // ดึงไฟล์แรกจาก input
    const selectedFile = inputElement.files[0];

    // ตรวจสอบว่าเลือกไฟล์มาหรือไม่
    if (!selectedFile) return false;

    // ตรวจสอบว่า MIME type ของไฟล์อยู่ในรายการที่อนุญาตหรือไม่
    return allowedMimeTypes.includes(selectedFile.type);
}

/**
 * ตรวจสอบเนื้อหาไฟล์ว่ามีคำที่อาจมีความเสี่ยงหรือไม่
 * @param {HTMLInputElement} file - element input ชนิด type="file"
 * @returns {Promise<boolean>} - คืนค่า true หากเนื้อหาไฟล์ปลอดภัย, false หากพบคำที่อาจมีความเสี่ยง
 * @description
 * ฟังก์ชันนี้จะอ่านเนื้อหาไฟล์ที่ผู้ใช้เลือกจาก input element และตรวจสอบว่ามีคำที่อาจมีความเสี่ยงหรือไม่
 * หากพบคำที่อาจมีความเสี่ยง เช่น JavaScript, <script>, form, method, type submit จะคืนค่า false
 * สามารถแสดง Alert แจ้งเตือนได้ตามต้องการ
 * ตัวอย่างการใช้งาน:
 * ```javascript
 * const fileInput = document.getElementById('fileInput');
 * const isValid = await validateFileContentByRegex(fileInput); // ตรวจสอบเนื้อหาไฟล์
 */
export async function isValidateFileContentByRegex(inputElement) {
    // ดึงไฟล์แรกจาก input
    const selectedFile = inputElement.files[0];

    // ตรวจสอบว่าเลือกไฟล์มาหรือไม่
    if (!selectedFile) return false;

    // อ่านเนื้อหาไฟล์เป็นข้อความ
    const fileContents = await selectedFile.text();

    // กำหนด regex สำหรับตรวจจับคำที่อาจมีความเสี่ยง
    const scriptPattern = /javascript|<script>|<\/script>|function/i;
    const formPattern = /form|method=["']?(get|post)["']?|type=["']?submit["']?/i;

    // ตรวจสอบว่าเนื้อหามี pattern อันตรายหรือไม่
    return !(scriptPattern.test(fileContents) || formPattern.test(fileContents));
}

/**
 * @param {any} startDate
 * @param {any} endDate
 * @param {any} checkDate
 * @returns
 */
export function isValidateDateBetween(startDate, endDate) {
    try {
        // แปลงเป็น Date object
        const start = dayjs(startDate);
        const end = dayjs(endDate);

        //// ตรวจสอบความถูกต้อง
        //if (!start.isValid() || !end.isValid()) {
        //    return {
        //        valid: false,
        //        message: "รูปแบบวันที่ไม่ถูกต้อง"
        //    };
        //}

        console.log(start);
        console.log(end);
        console.log(start.isAfter(end));
        // ตรวจสอบว่าวันเริ่มต้นไม่เกินวันสิ้นสุด
        if (start.isAfter(end)) {
            return {
                valid: false,
                message: "วันที่เริ่มต้นต้องไม่เกินวันที่สิ้นสุด"
            };
        }

        return {
            valid: true,
            message: ""
        }
    } catch (err) {
        console.error("ตรวจสอบวันที่", err)
    }
}