//จัดการ AJAX / REST API
async function getCsrfToken() {
    const token = document.querySelector('input[name="__RequestVerificationToken"]');
    return token?.value ?? "";
}

//Service API calls Data

export async function FetchRewardPointAsync() {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/data/rewards/fetch/point", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token,
            },
            body: JSON.stringify(),
            credentials: "same-origin",
        });
       
        return await response.json();
    } catch (error) {
        console.error(error);
        return { success: false, message: error.message };
    }
}

export async function FetchListRewardPointAsync() {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/data/rewards/fetch/list/point", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token,
            },
            body: JSON.stringify(),
            credentials: "same-origin",
        });
        return await response.json();
    } catch (error) {
        console.error(error);
        return { success: false, message: error.message };
    }
}

export async function FetchListRewardPromotionAsync() {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/data/rewards/fetch/list/point/promotion", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token,
            },
            body: JSON.stringify(),
            credentials: "same-origin",
        });
        return await response.json();
    } catch (error) {
        console.error(error);
        return { success: false, message: error.message };
    }
}

export async function fetchQuotationReportList(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/data/reports/fetch/quotation/motor", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token,
            },
            body: JSON.stringify(data),
            credentials: "same-origin",
        });
        return await response.json();
    } catch (error) {
        console.error(error);
        return { success: false, message: error.message };
    }
}

export async function fetchQuotationReportDetail(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/data/reports/fetch/quotation/detail", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token,
            },
            body: JSON.stringify(data),
            credentials: "same-origin",
        });
        return await response.json();
    } catch (error) {
        console.error(error);
        return { success: false, message: error.message };
    }
}

export async function fetchMasterPlanDetails(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/data/premiums/fetch/masterplan/detail", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token,
            },
            body: JSON.stringify(data),
            credentials: "same-origin",
        });
        return await response.json();
    } catch (error) {
        console.error(error);
        return { success: false, message: error.message };
    }
}