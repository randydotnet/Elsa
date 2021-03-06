using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elsa.Models;

namespace Elsa.Expressions
{
    public class WorkflowExpressionEvaluator : IWorkflowExpressionEvaluator
    {
        private readonly IDictionary<string, IExpressionEvaluator> evaluators;

        public WorkflowExpressionEvaluator(IEnumerable<IExpressionEvaluator> evaluators)
        {
            this.evaluators = evaluators.ToDictionary(x => x.Syntax);
        }
        
        public async Task<T> EvaluateAsync<T>(WorkflowExpression<T> expression, WorkflowExecutionContext workflowExecutionContext, CancellationToken cancellationToken)
        {
            var evaluator = evaluators[expression.Syntax];
            return await evaluator.EvaluateAsync<T>(expression.Expression, workflowExecutionContext, cancellationToken);
        }
    }
}