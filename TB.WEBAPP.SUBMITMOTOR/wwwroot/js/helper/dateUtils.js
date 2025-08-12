/**
 * โมดูลสำหรับจัดการวันที่ในรูปแบบ พ.ศ. (Buddhist Era) โดยใช้ dayjs และ flatpickr
 * @module dateUtils
 * @author Your Name
 * @description
 * โมดูลนี้ใช้สำหรับจัดการวันที่ในรูปแบบ พ.ศ. (Buddhist Era) โดยใช้ไลบรารี dayjs สำหรับการจัดการวันที่
 * และ flatpickr สำหรับการสร้าง date picker ที่รองรับการเลือกวันที่ในรูปแบบ พ.ศ.
 * * @requires dayjs
 * * @requires flatpickr
 * * @requires flatpickr/dist/flatpickr.css
 * * @requires flatpickr/dist/themes/material_blue.css
 * * @requires dayjs/locale/th
 * * @requires flatpickr/locale/th
 * * @example
 * import { formatDate, createThaiDatePicker } from './dateUtils';
 * * // แปลงวันที่เป็นรูปแบบ พ.ศ.
 * const formattedDate = formatDate(new Date(), 'D MMMM YYYY', true);
 * * // สร้าง date picker สำหรับวันที่ในรูปแบบ พ.ศ.
 * const datePicker = createThaiDatePicker('#dateInput', {
 *   minDate: '2020-01-01',
 *  maxDate: '2025-12-31',
 * defaultDate: '2023-01-01',
 * onChange: (selectedDates, dateStr, instance) => {
 * console.log('วันที่ถูกเลือก:', dateStr);
 * }
 */
dayjs.locale('th');

/**
 * ฟังก์ชันสำหรับแปลงวันที่เป็นรูปแบบที่กำหนด
 * @param {any} dateInput - วันที่ที่ต้องการแปลง สามารถเป็น Date, string หรือ dayjs object
 * @param {string} formatString - รูปแบบวันที่ที่ต้องการแสดง (ค่าเริ่มต้นคือ 'D MMMM YYYY')
 * @param {boolean} useThaiYear - ถ้าเป็น true จะใช้ปี พ.ศ. (Buddhist Era) แทนปี ค.ศ. (ค่าเริ่มต้นคือ false)
 * * @description
 * ฟังก์ชันนี้จะรับวันที่ในรูปแบบต่างๆ (Date, string หรือ dayjs object) และแปลงเป็นรูปแบบที่กำหนด
 * หาก `useThaiYear` เป็น true จะใช้ปี พ.ศ. (Buddhist Era) แทนปี ค.ศ.
 */
export function formatDate(dateInput, formatString = 'D MMMM YYYY', useThaiYear = false) {
    let date = dayjs(dateInput);
    if (!date.isValid()) return '';
    if (useThaiYear) {
        const buddhistYear = date.year() + 543;
        return date.format(formatString).replace(String(date.year()), String(buddhistYear));
    }
    return date.format(formatString);
}

/**
 * ฟังก์ชันสำหรับแปลงค่าวันที่เป็น Date จากค่าที่รับเข้ามาในรูปแบบ DD/MM/YYYY
 * @param {string} dateString - ค่าวันที่ในรูปแบบ DD/MM/YYYY
 * @returns {Date|null} - วันที่ถูกแปลงเป็น Date หรือ null ถ้าค่าไม่ถูกต้อง
 * @description
 * ฟังก์ชันนี้จะแปลงค่าวันที่เป็น Date จากค่าที่รับเข้ามาในรูปแบบ DD/MM/YYYY
 * ถ้าค่าไม่ถูกต้องจะคืนค่า null
 * เช่น
 * const date = convertStringToDate('01/01/2023');
 * console.log(date); // จะแสดงวันที่ 1 มกราคม 2023
 * จะแปลงค่าวันที่เป็น Date จากค่าที่รับเข้ามาในรูปแบบ DD/MM/YYYY
 * ถ้าค่าไม่ถูกต้องจะคืนค่า null
 */
export function convertStringToDate(dateString) {
    if (!dateString) return null;

    // แยกวัน เดือน ปี
    const parts = dateString.split('/');
    if (parts.length !== 3) return null;

    const day = parseInt(parts[0]);
    const month = parseInt(parts[1]);
    const year = parseInt(parts[2]);

    // สร้าง Date object (เดือนใน JavaScript เริ่มจาก 0)
    const date = new Date(year, month - 1, day);

    // ตรวจสอบความถูกต้อง
    if (date.getDate() !== day || date.getMonth() !== (month - 1) || date.getFullYear() !== year) {
        return null;
    }

    return date;
}

/**
 * ฟังก์ชันสำหรับแปลงวันที่เป็นรูปแบบ DD/MM/YYYY
 * @param {Date} date - วันที่ที่ต้องการแปลงเป็นรูปแบบ DD/MM/YYYY
 * @returns {string} - วันที่ถูกแปลงเป็นรูปแบบ DD/MM/YYYY
 * @description
 * ฟังก์ชันนี้จะแปลงวันที่เป็นรูปแบบ DD/MM/YYYY
 * ถ้าวันที่ไม่ถูกต้องจะคืนค่าว่าง
 * เช่น
 * const date = new Date('2023-01-01');
 * const formattedDate = convertDateToString(date);
 * console.log(formattedDate); // จะแสดงวันที่ 1 มกราคม 2023
 * จะแปลงวันที่เป็นรูปแบบ DD/MM/YYYY
 * ถ้าวันที่ไม่ถูกต้องจะคืนค่าว่าง
 */
export function convertDateToString(date, format = 'DD/MM/YYYY') {
    if (!date) return '';

    // ตรวจสอบว่าเป็น Date object หรือไม่
    if (!(date instanceof Date) && typeof date !== 'string') return '';

    const dayjsDate = dayjs(date);

    // ตรวจสอบความถูกต้อง
    if (!dayjsDate.isValid()) return '';

    return dayjsDate.format(format);
}

/**
 * สร้าง date picker สำหรับวันที่ในรูปแบบ พ.ศ. (Buddhist Era) โดยใช้ flatpickr
 * @param {string} selector - ตัวเลือก CSS สำหรับ element ที่ต้องการใช้ date picker
 * @param {Object} options - ตัวเลือกเพิ่มเติมสำหรับ date picker
 * @param {string|null} options.minDate - วันที่เริ่มต้น (ค่าเริ่มต้นคือ null)
 * @param {string|null} options.maxDate - วันที่สิ้นสุด (ค่าเริ่มต้นคือ null)
 * @param {string|null} options.defaultDate - วันที่เริ่มต้นที่จะแสดง (ค่าเริ่มต้นคือ null)
 * @param {function} options.onChange - ฟังก์ชันที่จะถูกเรียกเมื่อวันที่เปลี่ยนแปลง (ค่าเริ่มต้นคือ null)
 * @returns {FlatpickrInstance} - คืนค่าอินสแตนซ์ของ flatpickr ที่ถูกสร้างขึ้น
 * @description
 * ฟังก์ชันนี้จะสร้าง date picker ที่รองรับการเลือกวันที่ในรูปแบบ พ.ศ. (Buddhist Era)
 * และสามารถกำหนดตัวเลือกเพิ่มเติมได้ เช่น วันที่เริ่มต้น, วันที่สิ้นสุด, วันที่เริ่มต้นที่จะแสดง และฟังก์ชันที่จะถูกเรียกเมื่อวันที่เปลี่ยนแปลง
 */
export function createThaiDatePicker(selector, options = {}) {
    // ตรวจสอบว่า flatpickr ถูกโหลดหรือไม่
    if (typeof flatpickr === "undefined") {
        console.error("❌ flatpickr ยังไม่ถูกโหลด");
        return;
    }

    const config = {
        locale: "th",
        dateFormat: "d/m/Y",
        yearSelectorType: "dropdown",
        minDate: options.minDate || null,
        maxDate: options.maxDate || null,
        defaultDate: options.defaultDate || null,

        onChange: (selectedDates, dateStr, instance) => {
            if (typeof options.onChange === "function") {
                options.onChange(selectedDates, dateStr, instance);
            }
        },

        // กำหนดการแสดงปี พ.ศ.
        onReady: updateToBuddhistYear,
        onMonthChange: updateToBuddhistYear,
        onYearChange: updateToBuddhistYear,
        onOpen: updateToBuddhistYear,
    };

    return flatpickr(selector, config);
}

/**
 * อัปเดตปีใน input field ให้เป็นปี พ.ศ. และเพิ่ม event listener สำหรับการเปลี่ยนแปลง
 * @param {Date[]} selectedDates - วันที่ที่ถูกเลือก
 * @param {string} dateStr - วันที่ในรูปแบบ string
 * @param {FlatpickrInstance} instance - อินสแตนซ์ของ flatpickr
 * @description
 * ฟังก์ชันนี้จะอัปเดตปีใน input field ให้เป็นปี พ.ศ. โดยเพิ่ม 543 ปี
 * และเพิ่ม event listener สำหรับการเปลี่ยนแปลงปี
 */
function updateToBuddhistYear(selectedDates, dateStr, instance) {
    const yearInputs = instance.calendarContainer?.querySelectorAll(".numInput.flatpickr-year") || [];

    yearInputs.forEach(input => {
        const year = parseInt(input.value, 10);
        if (!isNaN(year)) {
            input.value = year + 543;
        }

        // ป้องกัน bind ซ้ำ
        if (!input.dataset.boundBuddhistYear) {
            input.addEventListener("change", e => {
                const newYear = parseInt(e.target.value, 10);
                if (!isNaN(newYear)) {
                    instance.changeYear(newYear - 543);
                }
            });
            input.dataset.boundBuddhistYear = "true";
        }
    });
}

