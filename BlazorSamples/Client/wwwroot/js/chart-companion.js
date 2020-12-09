class ColorPicker {
    static #colors = ['coral', 'lightgreen', 'skyblue', 'slateblue', 'gold'];
    static #index = Math.floor(Math.random() * 10);

    static get nextColor() {
        const idx = this.#index++ % this.#colors.length;
        return this.#colors[idx];
    }
}


export function createGraph(context, temparetures) {
    if (context.toString() !== '[object HTMLCanvasElement]') return

    const data = {
        labels: temparetures.labels,
        datasets: temparetures.datasets.map(d => (
            {
                label: d.label,
                data: d.data,
                tension: 0.5,
                borderColor: ColorPicker.nextColor,
            }
        ))
    };

    new Chart(context, {
        type: 'line',
        data: data,
        options: {
            responsive: true,
        }
    });
}
