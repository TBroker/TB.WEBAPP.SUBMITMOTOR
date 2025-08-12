//จัดการ DOM Event, User Interaction
import * as Pageloads from '../helper/pageLoader.js';
import * as ApiDatas from "../service/apiDatas.js";
import * as Utils from "../helper/utility.js";

export async function initRewardPoint() {
    // โหลดหน้าเว็บ
    await Pageloads.pageLoadFadeInModern(); // โหลดหน้าเว็บ
    // โหลด DOM
    await Pageloads.delay(2000).then(async () => {
        //✅ โหลด DOM ก่อน
        await initRewardCalculate(); // โหลดข้อมูลรางวัล
        await initRewardPromotion(); // โหลดรายการโปรโมชั่น
    }).finally(async () => {
        // โหลด fade out
        await Pageloads.pageLoadFadeOutModern(500); // โหลด fade out
    });
}

// โหลดรายการโปรโมชั่น
export async function initRewardPromotion() {
    const rewardPromotionData = await ApiDatas.FetchListRewardPromotionAsync();
    const promotionModel = document.getElementById("promotionModel");
    const promotionContainer = document.getElementById("promotionContainer");


    if (rewardPromotionData.data.length === 0) {
        rewardListContainer.innerHTML = "<p class='text-center'>ไม่มีข้อมูลรางวัล</p>";
        return;
    }

    let contentButton = "";
    let contentModel = "";

    rewardPromotionData.data.forEach((value, index) => {
        contentButton += `<button type="button" title="โปรโมชั่น" class="btn btn-sm btn-outline-secondary position-relative rounded-pill" data-bs-target="#model${index}" data-bs-toggle="modal">`;
        contentButton += `    ${value.promotion_name}`;

        if (value.flag_new === "Y") {
            contentButton += `<span class="position-absolute top-0 start-100 translate-middle p-2 badge rounded-pill bg-danger">
                                    <span>ใหม่</span>
                                    <span class="visually-hidden"></span>
                                </span>`;
        }

        contentButton += `</button>`;

        contentModel += `<div id="modal-promotion-list">
                            <div class="modal fade" id="model${index}" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog modal-dialog-centered modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-body p-0">
                                            <img src="${value.file_path_image}" class="img-fluid rounded" alt="${value.promotion_name}" asp-append-version="true">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>`;
    });

    promotionContainer.innerHTML = contentButton;
    promotionModel.innerHTML = contentModel;
}

// โหลดรายการรางวัล
export async function initRewardList(pointTotal) {
    const rewardListData = await ApiDatas.FetchListRewardPointAsync();
    const rewardListContainer = document.getElementById("rewardListContainer");

    if (rewardListData.data.length === 0) {
        rewardListContainer.innerHTML = "<p class='text-center'>ไม่มีข้อมูลรางวัล</p>";
        return;
    }

    let htmlContent = "";
    rewardListData.data.forEach((item) => {
        let isActive = Math.floor(pointTotal) >= Math.floor(item.point);
        htmlContent += `<div class="col-sm-6 col-12">
                            <div class="card card-body mb-3 align-items-center">
                            ${isActive ? `<span class="position-absolute top-0 start-100 translate-middle p-2 badge rounded-pill bg-danger"><i class="fa-solid fa-trophy"></i></span>` : ``}
                                <div class="position-relative">
                                    <img class="img-fluid ${isActive ? `` : `grayscale`}" src="${item.file_path_image}" alt="${item.header}" asp-append-version="true" />
                                    <div class="position-absolute top-50 start-50 translate-middle">
                                        <span class="fs-6 badge bg-secondary opacity-75">${item.header} <br /> ${Utils.formatToDisplayCurrency(Math.floor(item.point), false)} ${item.unit_pointed}</span>
                                    </div>
                                </div>
                            </div>
                        </div>`;
    });
    rewardListContainer.innerHTML = htmlContent;
}

// โหลดข้อมูลรางวัล
export async function initRewardCalculate() {
    // ✅ โหลดข้อมูลรางวัล
    const rewardData = await ApiDatas.FetchRewardPointAsync();

    const pointSpan = document.getElementById("pointSpan");
    const pointPlusSpan = document.getElementById("pointPlusSpan");
    const pointMonthlySpan = document.getElementById("pointMonthlySpan");
    const pointTotalSpan = document.getElementById("pointTotalSpan");
    const dateAsOfSpan = document.getElementById("dateAsOfSpan");

    if (rewardData.data[0].date_as_of === null || rewardData.data[0].date_as_of === "") {
        dateAsOfSpan.innerHTML = "(ข้อมูล ณ วันที่ไม่ระบุ)";
    } else {
        const dateAsOf = new Date(rewardData.data[0].date_as_of).toLocaleDateString('th-TH', {
            year: 'numeric',
            month: '2-digit',
            day: 'numeric'
        });
        dateAsOfSpan.innerHTML = `(ข้อมูล ณ วันที่ ${dateAsOf})`;
    }

    const point = rewardData.data[0].point; // จำนวนคะแนนปัจจุบัน
    const pointPlus = rewardData.data[0].point_plus; // จำนวนคะแนนพิเศษ
    const pointMonthly = rewardData.data[0].point_monthly; // จำนวนคะแนนรายเดือน
    const totalPoints = rewardData.data[0].point + rewardData.data[0].point_plus + rewardData.data[0].point_monthly; // คะแนนรวมทั้งหมด

    counterNumber(pointSpan.id, point, 2000);
    counterNumber(pointPlusSpan.id, pointPlus, 2000);
    counterNumber(pointMonthlySpan.id, pointMonthly, 2000);
    counterNumber(pointTotalSpan.id, totalPoints, 2000);

    await initRewardList(totalPoints)
}

// ฟังก์ชันสำหรับนับตัวเลขแบบอนิเมชั่น
function counterNumber(elementId, target, duration) {
    const element = document.getElementById(elementId); // รับ element ตาม ID
    let start = 0; // เริ่มต้นที่ 0
    const stepTime = 20; // กำหนดเวลาในการอัพเดตแต่ละขั้นตอน (20 มิลลิวินาที)
    const steps = duration / stepTime; // คำนวณจำนวนขั้นตอนที่ต้องทำ
    const increment = target / steps; // คำนวณการเพิ่มขึ้นในแต่ละขั้นตอน

    // ตรวจสอบว่า element มีอยู่หรือไม่
    const timer = setInterval(() => {
        start += increment;
        if (start >= target) {
            start = target;
            clearInterval(timer);
        }
        element.textContent = Utils.formatToDisplayCurrency(Math.floor(start), false);
    }, stepTime);
}  