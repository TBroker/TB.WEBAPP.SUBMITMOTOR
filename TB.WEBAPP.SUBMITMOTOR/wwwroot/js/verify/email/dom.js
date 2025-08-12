import * as Events from "./event.js";

export async function initForm() {
    // ✅ โหลด DOM ก่อน

    // ✅ ค่อย bind event หลัง DOM มี แล้ว
    await Events.initSubmitConfrim(); // Event: ฟอร์มใบเสนอราคา
}