// Grafico de Sustentabilidade (estilo BAR semelhante ao seu snippet)
// Espera a variável global:
//   window.grauPorcentagem (number) OR window.grauSustentabilidade (compatibilidade)
// Renderiza como BAR se existir canvas id="barSustentabilidadeChart"

(function () {
    function ready() {
        try {
            if (typeof Chart === 'undefined') {
                console.warn('Chart.js não carregado. Gráfico de sustentabilidade não será renderizado.');
                return;
            }

            // tenta obter valor de percentual de várias fontes possíveis
            let pct = null;
            if (typeof window.grauPorcentagem !== 'undefined') pct = window.grauPorcentagem;
            if ((pct === null || pct === undefined) && typeof window.grauSustentabilidade !== 'undefined') pct = window.grauSustentabilidade;
            // parse em número
            pct = parseFloat(pct);
            if (!isFinite(pct)) pct = 0;

            const canvas = document.getElementById('barSustentabilidadeChart') || document.getElementById('sustentabilidadeChart') || null;
            if (!canvas) {
                console.warn('[grafico_sustentabilidade] canvas não encontrado (ids esperados: barSustentabilidadeChart, sustentabilidadeChart).');
                return;
            }

            const ctx = canvas.getContext('2d');

            // destrói instância anterior se existir (evita duplicates)
            if (canvas._chartInstance) {
                try { canvas._chartInstance.destroy(); } catch (ex) { console.warn('erro ao destruir chart anterior', ex); }
            }

            // Se o canvas id for barSustentabilidadeChart, renderiza no estilo BAR (seu snippet)
            const isBar = canvas.id === 'barSustentabilidadeChart';

            if (isBar) {
                canvas._chartInstance = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: ['Sustentabilidade'],
                        datasets: [{
                            label: 'Grau de Sustentabilidade (%)',
                            data: [pct],
                            backgroundColor: 'rgba(112, 171, 105, 0.7)',
                            borderColor: 'rgba(69, 104, 64, 1)',
                            borderWidth: 2,
                            borderRadius: 6,
                            maxBarThickness: 60
                        }]
                    },
                    options: {
                        responsive: true,
                        plugins: {
                            legend: { display: false },
                            tooltip: {
                                callbacks: {
                                    label: ctx => (ctx.raw !== undefined ? ctx.raw + " %" : ctx.formattedValue + " %")
                                }
                            }
                        },
                        scales: {
                            y: {
                                beginAtZero: true,
                                max: 100,
                                ticks: {
                                    callback: val => val + '%'
                                },
                                title: {
                                    display: true,
                                    text: 'Porcentagem'
                                }
                            }
                        }
                    }
                });
                return;
            }

            // Fallback: doughnut (se preferir)
            const remaining = Math.max(0, 100 - pct);
            canvas._chartInstance = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ['Sustentabilidade (%)', 'Resto'],
                    datasets: [{
                        data: [pct, remaining],
                        backgroundColor: ['rgba(40, 167, 69, 0.9)', 'rgba(220,220,220,0.6)'],
                        hoverBackgroundColor: ['rgba(40,167,69,1)', 'rgba(200,200,200,0.8)']
                    }]
                },
                options: {
                    responsive: true,
                    cutout: '65%',
                    plugins: {
                        legend: { display: false },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'centerText',
                    beforeDraw: function (chart) {
                        const width = chart.width, height = chart.height;
                        const ctx2 = chart.ctx;
                        ctx2.restore();
                        const fontSize = Math.max(12, Math.floor(height / 8));
                        ctx2.font = `${fontSize}px sans-serif`;
                        ctx2.textBaseline = "middle";
                        const text = pct.toFixed(1) + "%";
                        const textX = Math.round((width - ctx2.measureText(text).width) / 2);
                        const textY = height / 2;
                        ctx2.fillStyle = '#000';
                        ctx2.fillText(text, textX, textY);
                        ctx2.save();
                    }
                }]
            });

        } catch (err) {
            console.error('[grafico_sustentabilidade] erro ao renderizar:', err);
        }
    }

    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', ready);
    else ready();
})();