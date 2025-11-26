// Grafico de Indicadores (estilo polarArea similar ao snippet PHP antigo)
// Usa variáveis globais definidas pela view:
//   window.indicadorLabels  (array de strings)  || window.labels
//   window.indicadorValores (array de numbers)  || window.dataValores
//   window.palette / window.backgroundColors (array de cores rgba)
// Procura por canvas id "polarChart" ou "indicadoresChart".

(function () {
    function ready() {
        try {
            if (typeof Chart === 'undefined') {
                console.warn('Chart.js não carregado. Grafico de indicadores não será renderizado.');
                return;
            }

            const labels = window.indicadorLabels || window.labels || [];
            const dataValores = window.indicadorValores || window.dataValores || [];
            const backgroundColors = window.palette || window.backgroundColors || [
                'rgba(255, 99, 132, 0.5)',
                'rgba(54, 162, 235, 0.5)',
                'rgba(255, 205, 86, 0.5)',
                'rgba(75, 192, 192, 0.5)',
                'rgba(153, 102, 255, 0.5)',
                'rgba(255, 159, 64, 0.5)',
                'rgba(201, 203, 207, 0.5)'
            ];

            // seleciona canvas (compatível com nomes antigos e novos)
            const canvas = document.getElementById('polarChart') || document.getElementById('indicadoresChart');
            if (!canvas) {
                console.warn('Canvas para gráfico de parâmetros não encontrado (esperado: #polarChart ou #indicadoresChart).');
                return;
            }
            const ctx = canvas.getContext('2d');

            // destrói instância anterior se existir
            if (canvas._chartInstance) {
                try { canvas._chartInstance.destroy(); } catch (e) { /* ignore */ }
            }

            // configurações base (parecidas com seu snippet PHP)
            const cfg = {
                type: 'polarArea',
                data: {
                    labels: labels,
                    datasets: [{
                        label: 'Pontuação do Indicador',
                        data: dataValores,
                        backgroundColor: backgroundColors.slice(0, labels.length)
                    }]
                },
                options: {
                    responsive: true,
                    maintainAspectRatio: false,
                    layout: {
                        padding: {
                            top: 20,
                            bottom: 20,
                            left: 20,
                            right: 20
                        }
                    },
                    plugins: {
                        legend: {
                            position: 'top'
                        },
                        title: {
                            display: true,
                            text: 'Pontuação dos Indicadores'
                        },
                        tooltip: {
                            callbacks: {
                                label: function (ctx) {
                                    const idx = ctx.dataIndex;
                                    const label = ctx.label || '';
                                    const value = ctx.formattedValue || ctx.raw;
                                    const param = (window.indicadorParametros && window.indicadorParametros[idx]) ? ' — ' + window.indicadorParametros[idx] : '';
                                    return `${label}: ${value}${param}`;
                                }
                            }
                        }
                    },
                    scales: {
                        r: {
                            pointLabels: {
                                display: true,
                                font: { size: 14 }
                            },
                            beginAtZero: true,
                            // define max como 5 (ou maior caso os dados excedam)
                            suggestedMin: 0,
                            suggestedMax: Math.max(5, ...(dataValores.length ? dataValores : [5]))
                        }
                    }
                }
            };

            // cria o chart e guarda referência no elemento para possível destruição futura
            canvas._chartInstance = new Chart(ctx, cfg);
        } catch (err) {
            console.error('Erro ao renderizar grafico_parametro:', err);
        }
    }

    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', ready);
    else ready();
})();