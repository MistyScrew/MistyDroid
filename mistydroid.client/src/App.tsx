import { useEffect, useState } from 'react';
import './App.css';


function App() {
    //const [screenshot, setScreenshot] = useState<Uint8Array>();
    const [screenshotId, setScreenshotId] = useState<number>();

    useEffect(() => {
        setScreenshotId(1);
    }, []);

    const contents = screenshotId === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : (
        <div>
                <img src={`api/session/gumballs/screenshot/${screenshotId}`} width="500px" />
                <button onClick={refreshScreenshot} >refresh</button>
        </div>
        );

    return (
        <div>
            {/*<h1 id="tableLabel">MistyDroid</h1>*/}
            {contents}
        </div>
    );

    function refreshScreenshot() {
        setScreenshotId((screenshotId ?? 0) + 1);
    }

    //async function populateScreenshot() {
    //    const response = await fetch('api/session/gumballs/screenshot');
    //    const screenshot = await response.bytes();
    //    setScreenshot(screenshot);
    //}

}

export default App;