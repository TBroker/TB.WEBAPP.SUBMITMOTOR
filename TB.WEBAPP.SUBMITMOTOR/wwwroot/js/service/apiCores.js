//จัดการ AJAX / REST API
async function getCsrfToken() {
    const token = document.querySelector('input[name="__RequestVerificationToken"]');
    return token?.value ?? "";
}

//Service API calls Core System
export async function fetchAgentDetails() {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/agent/fetch/detail", {
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

export async function fetchPrename(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/prename", {
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

export async function fetchOccupation(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/occupation", {
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

export async function fetchRelationship() {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/relationship", {
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

export async function fetchProvince() {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/province", {
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

export async function fetchDistrict(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/district", {
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

export async function fetchSubDistrict(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/subdistrict", {
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

export async function fetchQuotationDetail(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/quotation/fetch/detail", {
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

export async function fetchCarBrand(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/car/brand", {
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

export async function fetchCarModel(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/car/model", {
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

export async function fetchCarColor(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/car/color", {
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

export async function fetchCarVoluntaryCode() {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/master/fetch/car/voluntary/code", {
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

export async function FetchCalculatePeriod(data) {
    const token = await getCsrfToken();
    try {
        const response = await fetch("/api/installment/fetch/calculate/period", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token,
            },
            body: JSON.stringify(data),
            credentials: "same-origin",
        });
        return await response.json(data);
    } catch (error) {
        console.error(error);
        return { success: false, message: error.message };
    }
}

//export async function createUser(data) {
//    const token = getCsrfToken();
//    try {
//        const response = await fetch("/User/CreateUser", {
//            method: "POST",
//            headers: {
//                "Content-Type": "application/json",
//                "RequestVerificationToken": token,
//            },
//            body: JSON.stringify(data),
//            credentials: "same-origin",
//        });
//        if (!response.ok) {
//            const errorData = await response.json();
//            throw new Error(errorData.message || "Error creating user");
//        }
//        return await response.json();
//    } catch (error) {
//        console.error(error);
//        return { success: false, message: error.message };
//    }
//}

//export async function createUser(data) {
//    const response = await fetch("/User/CreateUser", {
//        method: "POST",
//        headers: {
//            "Content-Type": "application/json",
//        },
//        body: JSON.stringify(data),
//    });
//    return await response.json();
//}