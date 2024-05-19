
import { useNavigate } from "react-router-dom";

export default function Home() {

    const navigate = useNavigate();

    const goToNewRequestPage = () => {

        console.log('check!');
    }
    return (
        <>
            <div> this is Home page</div>
            <button color="green" onClick={goToNewRequestPage}>+</button>
        </>

    )
}
