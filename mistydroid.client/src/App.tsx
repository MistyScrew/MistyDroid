import { useEffect, useState } from 'react';
import './App.css';


function App() {
    const [screenshotId, setScreenshotId] = useState<number>();

    useEffect(() => {
        setScreenshotId(1);
    }, []);

    const contents = screenshotId === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : (
            <div>
                <img src={`api/session/gumballs/screenshot/${screenshotId}`} width="600px" onMouseDown={screenshotClick} />
                <button onClick={refreshScreenshot} >refresh</button>
        </div>
        );

    return (
        <div>
            {/*<h1 id="tableLabel">MistyDroid</h1>*/}
            {contents}
        </div>
    );

    function screenshotClick(event: React.MouseEvent<HTMLImageElement, MouseEvent>) {
        const k = 900 / 600;

        console.log(event);

        gameClick(Math.round(event.nativeEvent.offsetX * k), Math.round(event.nativeEvent.offsetY * k));
    }

    function refreshScreenshot() {
        setScreenshotId((screenshotId ?? 0) + 1);
    }

    async function gameClick(x: number, y: number) {
        await fetch('api/session/gumballs/click',
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({ point: { x: x, y: y } }),
            });

        await sleep(700);

        refreshScreenshot();
    }

    function sleep(ms: number) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }

}

export default App;