﻿class ColorPicker {
    static #colors = ['coral', 'lightgreen', 'skyblue', 'slateblue', 'gold'];
    static #index = Math.floor(Math.random() * 10);

    static get nextColor() {
        const idx = this.#index++ % this.#colors.length;
        return this.#colors[idx];
    }
}


export function createGraph(context, graph) {
    if (context.toString() !== '[object HTMLCanvasElement]') return

    new Chart(context, graph);
}
