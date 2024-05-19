
import { useNavigate } from "react-router-dom";

export default function CommunitiesMap() {

    const navigate = useNavigate();

    let regularRegisterObj = {
        isManager: false,
        communityId: 4, // Fix ! ---> need to take the communityId from the map!!
        inpCssClass: 'inpYellow',
        selectCssClass: 'selectYellow',

        btnCssId: 'btnToRegYellow',
        inpAboutCssId: 'inpAboutYellow',
        btnText: 'הרשמה'
    }
    let managerRegisterObj = {
        isManager: true,
        inpCssClass: 'inpPurple',
        selectCssClass: 'selectPurple',

        btnCssId: 'btnToNextLevelPurple',
        inpAboutCssId: 'inpAboutPurple',
        btnText: 'לשלב הבא'
    }


    return (
        <div style={{ border: 'black 1px solid' }}>

            <button style={{ backgroundColor: 'aqua' }} onClick={() => (navigate('/Register', { state: regularRegisterObj }))}>
                קהילה קיימת
            </button>
            <br />
            <br />
            <button style={{ backgroundColor: 'red' }} onClick={() => (navigate('/Register', { state: managerRegisterObj }))}>
                קהילה חדשה
            </button>
        </div>
    )
}
